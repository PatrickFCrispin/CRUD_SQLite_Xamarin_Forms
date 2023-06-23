using StudentCrudWithSQLite.Models;
using StudentCrudWithSQLite.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    public class NewStudentViewModel : BaseViewModel
    {
        public NewStudentViewModel()
        {
            Title = "Novo aluno";
            CancelCommand = new Command(async () => await GoToRouteAsync($"//{nameof(StudentListPage)}"));
            SaveCommand = new Command(async () => await OnSaveAsync(), ValidateSave);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async Task OnSaveAsync()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            var student = new Student
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Email = Email
            };

            try
            {
                var success = await StudentStore.NewStudentAsync(student);
                if (!success)
                {
                    await Shell.Current.DisplayAlert("ERRO", Messages.UnableToAddStudent, "OK");
                    return;
                }

                await Shell.Current.DisplayAlert("SUCESSO", Messages.StudentSuccessfullyAdded, "OK");
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
            public const string UnableToAddStudent = "Ocorreu um erro ao adicionar o aluno.";
            public const string StudentSuccessfullyAdded = "Aluno criado e adicionado com sucesso!";
        }
    }
}