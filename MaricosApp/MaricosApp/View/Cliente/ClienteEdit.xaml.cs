using MaricosApp.Models;
using MaricosApp.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Cliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClienteEdit : Rg.Plugins.Popup.Pages.PopupPage
	{
        private ClienteViewModel clienteViewModel;
        public ClienteEdit (ClienteRequest cliente)
		{
			InitializeComponent ();
            clienteViewModel = new ClienteViewModel();
            entryNombre.Text = cliente.nombre;
			entryApellido.Text = cliente.apellido;
			entryCalle.Text = cliente.calle;
			entryColonia.Text = cliente.colonia;
            entryTelefono.Text= cliente.telefono;
			entryNumero.Text = cliente.numero;
			entryReferencia.Text = cliente.referencia;
        }

        private void Cerrar(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
        private async void EditarCli(object sender, EventArgs e)
        {
            var clienteActualizado = new ClienteRequest
            {
                nombre = entryNombre.Text,
                apellido = entryApellido.Text,
                calle = entryCalle.Text,
                colonia = entryColonia.Text,
                telefono = entryTelefono.Text,
                numero = entryNumero.Text,
                referencia = entryReferencia.Text,
            };
            await clienteViewModel.ActualizarClienteAsync(clienteActualizado);
            await DisplayAlert("Resultado", "Datos Actualizados", "OK");
            await PopupNavigation.Instance.PopAsync();
        }

    }
}