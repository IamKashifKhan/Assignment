using Xamarin.Forms;
using Assignment.ViewModels;
using Assignment.Controls;

namespace Assignment.Views
{
    public partial class EditorPage : ContentPage
    {
        EditorViewModel _viewModel;

        public EditorPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EditorViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           // _viewModel.OnAppearing();
        }

       async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var htmlContent = await editorView.EvaluateJavaScriptAsync("alert('Called from Button()');");
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
