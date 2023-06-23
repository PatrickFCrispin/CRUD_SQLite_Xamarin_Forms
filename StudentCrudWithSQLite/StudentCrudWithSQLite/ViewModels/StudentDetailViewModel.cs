using StudentCrudWithSQLite.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class StudentDetailViewModel : BaseViewModel
    {
        public Command RemoveCommand { get; }
        public Command EditCommand { get; }

        public StudentDetailViewModel()
        {
            Title = "Detalhes";
            BackCommand = new Command(async () => await GoToRouteAsync($"//{nameof(StudentListPage)}"));
            LoadItemsCommand = new Command(async () => await LoadStudentByAsync(Id));
            RemoveCommand = new Command(async () => await OnRemoveAsync());
            EditCommand = new Command(async () => await GoToRouteAsync($"{nameof(EditStudentPage)}?{nameof(Id)}={Id}"));
        }

        private async Task LoadStudentByAsync(string id)
        {
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
                await GoToRouteAsync($"//{nameof(StudentListPage)}");
            }
        }

        private async Task OnRemoveAsync()
        {
            try
            {
                var success = await Shell.Current.DisplayAlert("INFO", Messages.WantToRemoveStudent, "OK", "Cancelar");
                if (!success) { return; }

                success = await StudentStore.RemoveStudentAsync(Id);
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
        }

        private static class Messages
        {
            public const string WantToRemoveStudent = "Tem certeza que deseja remover este aluno?";
            public const string UnableToRemoveStudent = "Ocorreu um erro ao remover o aluno.";
            public const string StudentSuccessfullyRemoved = "Aluno removido com sucesso!";
        }
    }
}