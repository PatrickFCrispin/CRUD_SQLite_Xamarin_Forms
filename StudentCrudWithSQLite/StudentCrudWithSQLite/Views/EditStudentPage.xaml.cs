using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentCrudWithSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStudentPage : ContentPage
    {
        readonly EditStudentViewModel _viewModel;

        public EditStudentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EditStudentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}