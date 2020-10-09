using Newtonsoft.Json;
using SampleApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SampleApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PersonModel> _items = new ObservableCollection<PersonModel>();

        private Predicate<object> _filter;

        private ObservableCollection<string> _sexChoices = new ObservableCollection<string>();
        private string _selectedSex;
        private bool _mustHaveProfilePicture;
        private double _minimumAge;

        public MainPageViewModel()
        {
            _sexChoices.Add("Any");
            _sexChoices.Add("Male");
            _sexChoices.Add("Female");

            _selectedSex = _sexChoices.First();

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

                    RaisePropertyChanged();
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

                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<string> SexChoices => _sexChoices;

        public string SelectedSex
        {
            get => _selectedSex;
            set
            {
                if (_selectedSex != value)
                {
                    _selectedSex = value;

                    RaisePropertyChanged();
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

                    RaisePropertyChanged();
                }
            }
        }

        public double MinimumAge
        {
            get => _minimumAge;
            set
            {
                if (_minimumAge != value)
                {
                    _minimumAge = value;

                    RaisePropertyChanged();
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

                        Items = new ObservableCollection<PersonModel>(people.Take(100));
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

            // Approximate age calculation
            var age = DateTime.Now.Year - person.Dob.Year;

            if (age < MinimumAge)
            {
                return false;
            }

            return true;
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
