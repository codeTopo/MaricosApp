
using MaricosApp.Models;
using MaricosApp.View.Cliente;
using MaricosApp.View.Concepto;
using MaricosApp.View.ProductoPopup;
using MaricosApp.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage1 : ContentPage
	{
        private ProductosViewModel viewModel;
        
        public HomePage1 ()
		{
			InitializeComponent ();
            viewModel = new ProductosViewModel();
            BindingContext = viewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((ProductosViewModel)BindingContext).InicializarAsync();

        }

        private async void MostrarView(object sender, EventArgs e)
        {
            // Duración de la animación en milisegundos (ajusta según sea necesario)
            uint animationTime = 2000;

            // Calcular la posición final en función de la altura de la página y el encabezado del contenido
            double pageHeight = Application.Current.MainPage.Height;
            double headerHeight = hiddenFrame.Height; 
            double finalY = pageHeight - headerHeight;

            // Mostrar el Frame con animación de desplazamiento
            await Task.WhenAll(
                hiddenFrame.TranslateTo(0, finalY, animationTime, Easing.SpringOut),
                Task.Delay((int)animationTime)
            );

            // Después de la animación, hacer visible el Frame
            hiddenFrame.IsVisible = true;
        }

        private async void ContraerButton_Clicked(object sender, EventArgs e)
        {
            uint animationTime =  1500;
            await Task.WhenAll(
               hiddenFrame.TranslateTo(0, 0, animationTime, Easing.SpringIn),
               Task.Delay((int)animationTime)
           );
            hiddenFrame.IsVisible = false;
        }

        private async void ClienteButon(object sender, EventArgs e)
        {
            try
            {
                string idClienteStr = await SecureStorage.GetAsync("idCliente");

                if (!string.IsNullOrEmpty(idClienteStr))
                {
                    // Si hay un idCliente guardado, navegar a ClientesView
                    await Navigation.PushAsync(new ClientesView());
                }
                else
                {
                    // Si no hay idCliente guardado, mostrar el Popup
                    await DisplayAlert("Error", "Favor De guardar Los Datos De Envio", "Ok");
                    await PopupNavigation.Instance.PushAsync(new ClientePopup());
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales aquí
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConceptoView());
        }
    }
}