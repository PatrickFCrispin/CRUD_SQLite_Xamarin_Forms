namespace StudentCrudWithSQLite.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string DevelopedBy { get; }

        public AboutViewModel()
        {
            Title = "Sobre";
            DevelopedBy = Messages.DevelopedBy;
        }

        private static class Messages
        {
            public const string DevelopedBy = "Desenvolvido por Patrick da F. Crispin.";
        }
    }
}