using System.ComponentModel;
using Xamarin.Forms;
using Assignment.ViewModels;

namespace Assignment.Views
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
