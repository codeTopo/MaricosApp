using MaricosApp.ViewModel;
using Xamarin.Forms;

namespace MaricosApp
{
    
    public partial class MainPage : ContentPage
    {
        private ProductosViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new ProductosViewModel();
            BindingContext = viewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((ProductosViewModel)BindingContext).InicializarAsync();
        }

    }
}
