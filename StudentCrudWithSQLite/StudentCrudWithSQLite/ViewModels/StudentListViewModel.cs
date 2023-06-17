using StudentCrudWithSQLite.Models;
using StudentCrudWithSQLite.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    public class StudentListViewModel : BaseViewModel
    {
        private bool _isListEmpty;
        private string _listEmptyMessage;

        public bool IsListEmpty
        {
            get => _isListEmpty;
            private set { SetProperty(ref _isListEmpty, value); }
        }

        public string ListEmptyMessage
        {
            get => _listEmptyMessage;
            private set { SetProperty(ref _listEmptyMessage, value); }
        }

        public ObservableCollection<Student> Items { get; }
        public Command<Student> ItemTapped { get; }
        public Command AddItemCommand { get; }

        public StudentListViewModel()
        {
            Title = "Alunos";
            Items = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await LoadStudentListAsync());
            ItemTapped = new Command<Student>(async (student) => await GoToRouteAsync($"{nameof(StudentDetailPage)}?{nameof(Id)}={student.Id}"));
            AddItemCommand = new Command(async () => await GoToRouteAsync(nameof(NewStudentPage)));
        }

        private async Task LoadStudentListAsync()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                var students = await StudentStore.GetAllStudentsAsync();
                if (!students.Any())
                {
                    IsListEmpty = true;
                    ListEmptyMessage = Messages.ListEmpty;
                    IsBusy = false;
                    return;
                }

                HandleSuccessfulLoadingOf(students);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERRO", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void HandleSuccessfulLoadingOf(IEnumerable<Student> students)
        {
            IsListEmpty = false;
            Items.Clear();
            foreach (var student in students)
            {
                Items.Add(student);
            }
        }

        private static class Messages
        {
            public const string ListEmpty = "Nenhum aluno cadastrado.";
        }
    }
}