using StudentCrudWithSQLite.Services;
using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentCrudWithSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentDetailPage : ContentPage
    {
        private readonly StudentDetailViewModel _viewModel;

        public StudentDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new StudentDetailViewModel(DependencyService.Get<IStudentStore>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}