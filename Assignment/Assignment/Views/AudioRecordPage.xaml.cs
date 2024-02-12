using Assignment.ViewModels;
using Xamarin.Forms;

namespace Assignment.Views
{
    public partial class AudioRecordPage : ContentPage
    {
        public AudioRecordPage()
        {
            InitializeComponent();

            BindingContext = new AudioRecordViewModel();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {

        }
    }
}
