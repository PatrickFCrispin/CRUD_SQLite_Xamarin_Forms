using StudentCrudWithSQLite.Views;
using Xamarin.Forms;

namespace StudentCrudWithSQLite
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(StudentListPage), typeof(StudentListPage));
            Routing.RegisterRoute(nameof(StudentDetailPage), typeof(StudentDetailPage));
            Routing.RegisterRoute(nameof(NewStudentPage), typeof(NewStudentPage));
            Routing.RegisterRoute(nameof(EditStudentPage), typeof(EditStudentPage));
        }
    }
}