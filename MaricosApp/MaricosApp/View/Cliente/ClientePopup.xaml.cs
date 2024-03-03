using MaricosApp.Models;
using MaricosApp.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientePopup : Rg.Plugins.Popup.Pages.PopupPage
	{
        private ClienteViewModel clienteViewModel;
        public ClientePopup ()
		{
			InitializeComponent ();
            clienteViewModel = new ClienteViewModel();

        }
        private void Cerrar(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            clienteViewModel = new ClienteViewModel();
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Crear un objeto ClienteRequest con los datos del formulario
            ClienteRequest nuevoCliente = new ClienteRequest
            {
                nombre = entryNombre.Text,
                apellido = entryApellido.Text,
                calle = entryCalle.Text,
                colonia = entryColonia.Text,
                numero = entryNumero.Text,
                telefono = entryTelefono.Text,
                referencia = entryReferencia.Text,
            };
            string resultado = await clienteViewModel.AgregarClienteAsync(nuevoCliente);
            if (resultado != null)
            {

                await DisplayAlert("Alerta", "Datos Agregagos con Exito", "OK");
                await clienteViewModel.ClienteId();
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                
            }
        }

    }
}