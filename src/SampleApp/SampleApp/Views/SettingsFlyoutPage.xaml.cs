using Xamarin.Forms;

namespace SampleApp.Views
{
    public partial class SettingsFlyoutPage : ContentPage
    {
        public SettingsFlyoutPage()
        {
            InitializeComponent();
        }

        private void MustHaveProfilePictureLabel_Tapped(object sender, System.EventArgs e)
        {
            MustHaveProfilePictureCheckBox.IsChecked = !MustHaveProfilePictureCheckBox.IsChecked;
        }
    }
}