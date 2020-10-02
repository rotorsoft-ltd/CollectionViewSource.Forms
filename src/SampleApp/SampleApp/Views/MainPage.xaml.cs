using Rotorsoft.Forms;
using SampleApp.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SampleApp.Views
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPageViewModel ViewModel => BindingContext as MainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
