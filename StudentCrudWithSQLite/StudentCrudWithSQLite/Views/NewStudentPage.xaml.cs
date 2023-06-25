using StudentCrudWithSQLite.Services;
using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentCrudWithSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewStudentPage : ContentPage
    {
        public NewStudentPage()
        {
            InitializeComponent();

            BindingContext = new NewStudentViewModel(
                DependencyService.Get<IStudentStore>());
        }
    }
}