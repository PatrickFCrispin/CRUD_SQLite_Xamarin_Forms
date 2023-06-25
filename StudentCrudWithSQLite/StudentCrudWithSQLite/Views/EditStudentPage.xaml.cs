using StudentCrudWithSQLite.Services;
using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentCrudWithSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStudentPage : ContentPage
    {
        private readonly EditStudentViewModel _viewModel;

        public EditStudentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EditStudentViewModel(DependencyService.Get<IStudentStore>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}