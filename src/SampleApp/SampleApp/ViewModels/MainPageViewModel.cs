using Newtonsoft.Json;
using SampleApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PersonModel> _items = new ObservableCollection<PersonModel>();

        private Predicate<object> _filter;

        private bool _mustHaveProfilePicture;

        public MainPageViewModel()
        {
            Filter = (p) => FilterPerson(p as PersonModel);
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

        public Predicate<object> Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;

                    RaisePropertyChanged(nameof(Filter));
                }
            }
        }

        public bool MustHaveProfilePicture
        {
            get => _mustHaveProfilePicture;
            set
            {
                if (_mustHaveProfilePicture != value)
                {
                    _mustHaveProfilePicture = value;

                    RaisePropertyChanged(nameof(MustHaveProfilePicture));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task LoadDataAsync()
        {
            using (var fileStream = await FileSystem.OpenAppPackageFileAsync("People.json"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        serializer.DateFormatString = "dd/MM/yyyy";
                        var people = serializer.Deserialize<PersonModel[]>(jsonReader);

                        Items = new ObservableCollection<PersonModel>(people);
                    }
                }
            }
        }

        private bool FilterPerson(PersonModel person)
        {
            if (person == null)
            {
                return false;
            }

            if (MustHaveProfilePicture && string.IsNullOrWhiteSpace(person.Avatar))
            {
                return false;
            }

            return true;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
