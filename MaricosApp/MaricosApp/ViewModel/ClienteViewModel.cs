using MaricosApp.Models;
using MaricosApp.Serivices;
using MaricosApp.View.Cliente;
using MaricosApp.View.ProductoPopup;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaricosApp.ViewModel
{
    public class ClienteViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<ClienteRequest> clientes;
        public List<ClienteRequest> Clientes
        {
            get { return clientes; }
            set
            {
                if (clientes != value)
                {
                    clientes = value;
                    OnPropertyChanged(nameof(Clientes));
                }
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                }
            }
        }

        public ClienteViewModel()
        {
            apiService = new ApiService();
        }
        public async Task InicializarAsync()
        {
            await ClienteId();
        }
        public async Task<string> ActualizarClienteAsync(ClienteRequest cliente)
        {
            try
            {
                return await ClientePut(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Se produjo un error inesperado al actualizar el cliente. Favor de intentar más tarde.";
            }
        }
        private string ValidarCliente(ClienteRequest cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.nombre))
            {
                return "El campo 'Nombre' es obligatorio.";
            }
            if (string.IsNullOrWhiteSpace(cliente.apellido))
            {
                return "El campo 'Apellido' es obligatorio.";
            }
            if (string.IsNullOrWhiteSpace(cliente.calle))
            {
                return "El campo 'Calle' es obligatorio.";
            }
            if (string.IsNullOrWhiteSpace(cliente.colonia))
            {
                return "El campo 'Colonia' es obligatorio.";
            }
            if (string.IsNullOrWhiteSpace(cliente.numero))
            {
                return "El campo 'Número' es obligatorio.";
            }
            if (string.IsNullOrWhiteSpace(cliente.telefono))
            {
                return "El campo 'Teléfono' es obligatorio.";
            }
            if (cliente.telefono.Length != 10)
            {
                return "El campo 'Teléfono' debe tener 10 dígitos.";
            }
            if (string.IsNullOrWhiteSpace(cliente.referencia))
            {
                return "El campo 'Referencia' es obligatorio.";
            }
            return string.Empty; // No hay errores de validación
        }


        public async Task<string> AgregarClienteAsync(ClienteRequest cliente)
        {
            try
            {
                string validationError = ValidarCliente(cliente);
                if (!string.IsNullOrEmpty(validationError))
                {
                    return validationError;
                }
                if (!await IsConnected())
                {
                    return "Error de conexión. Favor de revisar tu conexión a Internet y vuelve a intentarlo.";
                }
                string jsonContent = JsonConvert.SerializeObject(cliente);
                string endpoint = "/api/clientes/agregar";
                string response = await apiService.Post(endpoint, jsonContent);
                if (response != null)
                {
                    Console.WriteLine($"Respuesta del backend: {response}");
                    // Deserializar la respuesta JSON
                    var jsonResponse = JsonConvert.DeserializeObject<RespuestaAPi>(response);
                    if (jsonResponse.Exito == 1)
                    {
                        // Extraer el idCliente
                        long idCliente = jsonResponse.Data?.IdCliente ?? -1;
                        await SecureStorage.SetAsync("idCliente", idCliente.ToString());
                        return response;
                    }
                    else
                    {
                       
                        return jsonResponse.Mensaje ?? "Error en la respuesta del backend.";
                    }
                }
                else
                {
                    return "Error al realizar la solicitud HTTP. Favor de intentar más tarde.";
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error: {ex.Message}");
                return "Se produjo un error inesperado. Favor de intentar más tarde.";
            }
        }
        public async Task<string> ClienteId()
        {
            try
            {
                var idCliente = await SecureStorage.GetAsync("idCliente");
                if (string.IsNullOrEmpty(idCliente))
                {
                    return "No se encontró un ID de cliente válido.";
                }
                string endpoint = $"/api/clientes/{idCliente}";
                if (!await IsConnected())
                {
                    return "Error de conexión. Favor de revisar tu conexión a Internet y vuelve a intentarlo.";
                }
                string response = await apiService.Get(endpoint);
                var jsonResponse = JsonConvert.DeserializeObject<List<ClienteRequest>>(response);
                Clientes = jsonResponse;

                return "Búsqueda Exitosa";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Se produjo un error inesperado. Favor de intentar más tarde.";
            }
        }
        public async Task<string> ClientePut(ClienteRequest cliente)
        {
            try
            {
                IsBusy = true;
                string validationError = ValidarCliente(cliente);
                if (!string.IsNullOrEmpty(validationError))
                {
                    return validationError;
                }
                if (!await IsConnected())
                {
                    return "Error de conexión. Favor de revisar tu conexión a Internet y vuelve a intentarlo.";
                }
                string jsonContent = JsonConvert.SerializeObject(cliente);
                string idCliente = await SecureStorage.GetAsync("idCliente");
                if (string.IsNullOrEmpty(idCliente))
                {
                    var popup = new ClientePopup();
                    await App.Current.MainPage.Navigation.PushPopupAsync(popup);
                    return "No se encontró un ID de cliente válido.";

                }
                string endpoint = $"/api/clientes/{idCliente}";
                string response = await apiService.Put(endpoint, jsonContent);
                if (response != null)
                {
                    Console.WriteLine($"Respuesta del backend: {response}");
                    var jsonResponse = JsonConvert.DeserializeObject<RespuestaAPi>(response);
                    if (jsonResponse.Exito == 1)
                    {
                        return response;
                    }
                    else
                    {
                        return jsonResponse.Mensaje ?? "Error en la respuesta del backend.";
                    }
                }
                else
                {
                    return "Error al realizar la solicitud HTTP. Favor de intentar más tarde.";
                }
            }
            finally
            {
                IsBusy = false; // Oculta el indicador de carga
            }
        }

    }
}
