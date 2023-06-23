using StudentCrudWithSQLite.DataBases;
using StudentCrudWithSQLite.Services;
using Xamarin.Forms;

namespace StudentCrudWithSQLite
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<StudentDataStore>();
            DependencyService.Register<DataBase>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}