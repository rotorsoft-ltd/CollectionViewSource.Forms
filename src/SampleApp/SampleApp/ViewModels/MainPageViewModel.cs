using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _items = new ObservableCollection<string>();

        public MainPageViewModel()
        {
            _items.Add("a");
            _items.Add("b");
            _items.Add("c");

            FilterDelegate = (o) =>
            {
                return true;
            };
        }

        public ObservableCollection<string> Items
        {
            get =>  _items;
            set
            {
                if (_items != value)
                {
                    _items = value;

                    RaisePropertyChanged(nameof(Items));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Func<object, bool> FilterDelegate
        {
            get;
            set;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
