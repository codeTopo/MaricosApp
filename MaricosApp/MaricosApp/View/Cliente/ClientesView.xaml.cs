using MaricosApp.Models;
using MaricosApp.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesView : ContentPage
    {
        private ClienteViewModel viewModel;
        public ClientesView()
        {
            InitializeComponent();
            viewModel = new ClienteViewModel();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Realiza la solicitud al backend al abrir la página
            string resultado = await viewModel.ClienteId();

            // Verifica el resultado y toma las acciones necesarias
            if (resultado == "Búsqueda Exitosa" && viewModel.Clientes != null && viewModel.Clientes.Any())
            {
                ClienteRequest primerCliente = viewModel.Clientes[0];
                NombreLabel.Text = primerCliente.nombre;
                ApellidoLabel.Text = primerCliente.apellido;
                CalleLabel.Text = primerCliente.calle;
                ColoniaLabel.Text = primerCliente.colonia;
                NumeroLabel.Text = primerCliente.numero;
                TelefonoLabel.Text = primerCliente.telefono;
                ReferenciaLabel.Text = primerCliente.referencia;
            }
            else
            {
                // Manejar el caso en el que la solicitud no fue exitosa o la lista de clientes está vacía
                await DisplayAlert("Error", resultado, "OK");
            }
        }

        private async void VerDetallesButton_Clicked(object sender, EventArgs e)
        {
            // Abre el popup cuando se hace clic en el botón "Ver Detalles"
            var popup = new ClienteEdit(viewModel.Clientes[0]); // Puedes pasar datos al popup si es necesario
            await Navigation.PushPopupAsync(popup);
        }


    }
}