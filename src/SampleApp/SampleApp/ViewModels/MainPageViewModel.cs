using SampleApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PersonModel> _items = new ObservableCollection<PersonModel>();

        public MainPageViewModel()
        {
            FilterDelegate = (o) =>
            {
                // var c = (string)o;

                return true;
            };
        }

        public ObservableCollection<PersonModel> Items
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

        public Predicate<object> FilterDelegate
        {
            get;
            set;
        }

        public async Task LoadDataAsync()
        {
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
