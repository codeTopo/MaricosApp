using MaricosApp.Models;
using MaricosApp.View.ProductoPopup;
using MaricosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Venta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentaView : ContentPage
    {

        private string tipoDePago = "";
        

        public VentaView()
        {
            InitializeComponent();
            

        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox.IsChecked)
            {
                // Si el checkbox está marcado, actualiza el tipo de pago
                tipoDePago = checkBox.AutomationId; // Usar AutomationId para identificar el tipo de pago
            }
            else
            {
                // Si el checkbox está desmarcado, podrías implementar lógica adicional si es necesario
                tipoDePago = "Efectivo"; // Actualiza el tipo de pago a un valor por defecto o vacío
            }
        }

        private async void RealizarVenta_Clicked(object sender, EventArgs e)
        {
           
        }

    }
}
