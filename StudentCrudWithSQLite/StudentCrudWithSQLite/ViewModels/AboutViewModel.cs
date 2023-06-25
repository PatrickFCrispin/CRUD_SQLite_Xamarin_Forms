namespace StudentCrudWithSQLite.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _developedBy;

        public string DevelopedBy
        {
            get => _developedBy;
            private set { SetProperty(ref _developedBy, value); }
        }

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