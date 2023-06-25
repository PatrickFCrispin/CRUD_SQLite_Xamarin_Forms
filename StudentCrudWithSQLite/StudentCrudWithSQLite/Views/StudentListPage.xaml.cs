using StudentCrudWithSQLite.Services;
using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentCrudWithSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentListPage : ContentPage
    {
        private readonly StudentListViewModel _viewModel;

        public StudentListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new StudentListViewModel(DependencyService.Get<IStudentStore>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}