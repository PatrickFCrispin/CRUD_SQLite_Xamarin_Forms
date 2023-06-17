using StudentCrudWithSQLite.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _title = string.Empty;
        private string _id;
        private string _name;
        private string _email;

        public IStudentStore StudentStore => DependencyService.Get<IStudentStore>();

        public bool IsBusy
        {
            get => _isBusy;
            protected set { SetProperty(ref _isBusy, value); }
        }

        public string Title
        {
            get => _title;
            protected set { SetProperty(ref _title, value); }
        }

        public string Id
        {
            get => _id;
            set { SetProperty(ref _id, value); }
        }

        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        public string Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }

        public Command LoadItemsCommand { get; protected set; }
        public Command CancelCommand { get; protected set; }
        public Command SaveCommand { get; protected set; }
        public Command BackCommand { get; protected set; }

        protected async Task GoToRouteAsync(string root)
        {
            // This will goes to root view page of the navigation stack
            await Shell.Current.GoToAsync(root);
        }

        protected bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email);
        }

        protected bool SetProperty<T>(ref T backingStore, T value, 
            [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) { return false; }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null) { return; }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}