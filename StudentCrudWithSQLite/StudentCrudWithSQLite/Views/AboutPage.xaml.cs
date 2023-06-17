using StudentCrudWithSQLite.ViewModels;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = new AboutViewModel();
        }
    }
}