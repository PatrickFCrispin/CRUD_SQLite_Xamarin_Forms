using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentCrudWithSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentListPage : ContentPage
    {
        readonly StudentListViewModel _viewModel;

        public StudentListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new StudentListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}