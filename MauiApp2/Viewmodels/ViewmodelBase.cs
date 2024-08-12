using System.ComponentModel;

namespace MauiApp2.Viewmodels
{
    internal class ViewmodelBase : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
