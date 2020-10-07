
using FFImageLoading.Transformations;
using Rotorsoft.Forms;
using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp.Views
{
    public partial class DetailPage : ContentPage
    {
        private MainPageViewModel ViewModel => BindingContext as MainPageViewModel;

        public DetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
                await ViewModel.LoadDataAsync();
            }
        }

        protected override void OnDisappearing()
        {
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }

            base.OnDisappearing();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.MustHaveProfilePicture))
            {
                (Resources["DataSource"] as CollectionViewSource)?.View?.Refresh();
            }
        }
    }
}