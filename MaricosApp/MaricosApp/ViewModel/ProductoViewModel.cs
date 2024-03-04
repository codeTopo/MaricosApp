using MaricosApp.Models;
using MaricosApp.Serivices;
using MaricosApp.View.Producto;
using MaricosApp.View.ProductoPopup;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MaricosApp.ViewModel
{
    public class ProductosViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        public ObservableCollection<ProductoRequest> ProductList { get; set; } = new ObservableCollection<ProductoRequest>();

        //constructor general del viewmodel
        public ProductosViewModel()
        {
            apiService = new ApiService();

            InicilizarComandos();
           
        }

        private void InicilizarComandos()
        {
            SelectProductCommand = new Command<ProductoRequest>(async (product) =>
            {
                await App.Navigation.PushModalAsync(new ProductoPopup(product));
            });
        }

        //acciones del viewmodel
        public async Task InicializarAsync()
        {
            await CargarProductosAsync();
        }

        public Command<ProductoRequest> SelectProductCommand { get; set; }

        //solicitudes http al apiservice 
        public async Task CargarProductosAsync()
        {
            string response = await apiService.Get("/api/productos");
            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (apiResponse.exito == 1)
                    {
                        ProductList.Clear();
                        foreach (ProductoRequest producto in apiResponse.data)
                        {
                            Console.WriteLine("aqui" + apiResponse);
                            ProductList.Add(producto);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error en la respuesta: {apiResponse.mensaje}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al deserializar la respuesta: {ex}");
                    var message = new EmailMessage
                    {
                        Subject = "Error en la aplicación",
                        Body = $"Ocurrió un error al deserializar la respuesta: {ex}",
                        To = new List<string> { "tu-correo@gmail.com" } // Cambia esto a tu dirección de correo electrónico
                    };
                    await Email.ComposeAsync(message);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(
                            "Error",
                            "Hubo un problema al cargar los productos. Favor de contactar al Establecimiento con el numero 3951185963.",
                            "OK");
            }
        }
    }
}
