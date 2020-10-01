using System.ComponentModel;
using Xamarin.Forms;
using SampleApp.ViewModels;

namespace SampleApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}