using StudentCrudWithSQLite.Models;
using StudentCrudWithSQLite.Services;
using StudentCrudWithSQLite.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class EditStudentViewModel : BaseViewModel
    {
        private readonly IStudentStore _studentStore;

        public EditStudentViewModel(IStudentStore studentStore)
        {
            _studentStore = studentStore;
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
                var student = _studentStore.GetStudentBy(id);
                if (student is null)
                {
                    await Shell.Current.DisplayAlert("ERRO", Messages.UnableToGetStudent, "OK");
                    await GoToRouteAsync($"//{nameof(StudentListPage)}");
                    return;
                }

                Name = student.Name;
                Email = student.Email;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
            finally { IsBusy = false; }
        }

        private async Task OnSaveAsync()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                var success = _studentStore.UpdateStudent(new Student { Id = Id, Name = Name, Email = Email });
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
            public const string UnableToGetStudent = "Não foi possível recuperar os dados do aluno.";
            public const string UnableToUpdateStudent = "Não foi possível atualizar o aluno.";
            public const string StudentSuccessfullyUpdated = "Aluno atualizado com sucesso!";
        }
    }
}