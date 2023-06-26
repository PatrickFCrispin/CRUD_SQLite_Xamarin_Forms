using StudentCrudWithSQLite.Services;
using StudentCrudWithSQLite.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class StudentDetailViewModel : BaseViewModel
    {
        private readonly IStudentStore _studentStore;

        public Command RemoveCommand { get; }
        public Command EditCommand { get; }

        public StudentDetailViewModel(IStudentStore studentStore)
        {
            _studentStore = studentStore;
            Title = "Detalhes";
            BackCommand = new Command(async () => await GoToRouteAsync($"//{nameof(StudentListPage)}"));
            LoadItemsCommand = new Command(async () => await LoadStudentByAsync(Id));
            RemoveCommand = new Command(async () => await OnRemoveAsync());
            EditCommand = new Command(async () => await GoToRouteAsync($"{nameof(EditStudentPage)}?{nameof(Id)}={Id}"));
        }

        public StudentDetailViewModel()
        {
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

        private async Task OnRemoveAsync()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                var success = await Shell.Current.DisplayAlert("INFO", Messages.AsksIfWantToRemoveStudent, "OK", "Cancelar");
                if (!success) { return; }

                success = _studentStore.RemoveStudent(Id);
                if (!success)
                {
                    await Shell.Current.DisplayAlert("ERRO", Messages.UnableToRemoveStudent, "OK");
                    return;
                }

                await Shell.Current.DisplayAlert("SUCESSO", Messages.StudentSuccessfullyRemoved, "OK");
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
            public const string AsksIfWantToRemoveStudent = "Tem certeza que deseja remover este aluno?";
            public const string UnableToRemoveStudent = "Não foi possível remover o aluno.";
            public const string StudentSuccessfullyRemoved = "Aluno removido com sucesso!";
        }
    }
}