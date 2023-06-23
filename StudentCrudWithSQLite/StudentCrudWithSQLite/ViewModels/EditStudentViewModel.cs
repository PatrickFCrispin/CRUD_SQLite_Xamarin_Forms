using StudentCrudWithSQLite.Models;
using StudentCrudWithSQLite.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class EditStudentViewModel : BaseViewModel
    {
        public EditStudentViewModel()
        {
            Title = "Edição";
            BackCommand = new Command(async () => await GoToRouteAsync($"{nameof(StudentDetailPage)}?{nameof(Id)}={Id}"));
            LoadItemsCommand = new Command(async () => await LoadStudentByAsync(Id));
            CancelCommand = new Command(async () => await GoToRouteAsync($"//{nameof(StudentListPage)}"));
            SaveCommand = new Command(async () => await OnSaveAsync(), ValidateSave);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async Task LoadStudentByAsync(string id)
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                var student = await StudentStore.GetStudentByAsync(id);
                if (student != null)
                {
                    Name = student.Name;
                    Email = student.Email;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
                await GoToRouteAsync($"{nameof(StudentDetailPage)}?{nameof(Id)}={Id}");
            }
            finally { IsBusy = false; }
        }

        private async Task OnSaveAsync()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            var student = new Student
            {
                Id = Id,
                Name = Name,
                Email = Email
            };

            try
            {
                var success = await StudentStore.UpdateStudentAsync(student);
                if (!success)
                {
                    await Shell.Current.DisplayAlert("ERRO", Messages.UnableToUpdateStudent, "OK");
                    return;
                }

                await Shell.Current.DisplayAlert("SUCESSO", Messages.StudentSuccessfullyUpdated, "OK");
                await GoToRouteAsync($"//{nameof(StudentListPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
            finally { IsBusy = false; }
        }

        private static class Messages
        {
            public const string UnableToUpdateStudent = "Ocorreu um erro ao atualizar o aluno.";
            public const string StudentSuccessfullyUpdated = "Aluno atualizado com sucesso!";
        }
    }
}