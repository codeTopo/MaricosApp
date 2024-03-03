using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Essentials;

namespace MaricosApp.Serivices
{
    public class ApiService
    {
        private readonly string baseUrl = "https://mariscospepe.somee.com";
        private readonly HttpClient httpClient;

        public ApiService()
        {
            httpClient = new HttpClient(new TokenAuthorization());
        }

        private class TokenAuthorization : HttpClientHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                try
                {
                    var accessTokenJson = await SecureStorage.GetAsync("AccessToken");

                    if (!string.IsNullOrEmpty(accessTokenJson))
                    {
                        // Deserializar el JSON y obtener el token
                        var accessTokenObject = JsonConvert.DeserializeObject<JObject>(accessTokenJson);
                        var accessToken = accessTokenObject?.Value<string>("token");

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Agregar el token como Bearer Authorization
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                            Console.WriteLine(accessToken);
                        }
                    }

                    return await base.SendAsync(request, cancellationToken);
                }
                catch (Exception)
                {
                    // Manejar excepciones según sea necesario
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }
        public bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }

        //Solicitudes 
        public async Task<string> Get(string endpoint)
        {
            if (!IsConnected())
            {
                await App.Current.MainPage.DisplayAlert("Error", "Favor de revisar tu coneccion a internet y vuelvea intentarlo gracias  ", "OK");
                return null;
            }
            string fullUrl = baseUrl + endpoint;
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Data: {responseData}");
                    return responseData;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}, {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Manejar la excepción según sea necesario.
                return null;
            }
        }
        public async Task<string> Post(string endpoint, string jsonContent)
        {
            if (!IsConnected())
            {
                await App.Current.MainPage.DisplayAlert("Error", "Favor de revisar tu coneccion a internet y vuelvea intentarlo gracias  ", "OK");
                return null;
            }
            string fullUrl = baseUrl + endpoint;
            HttpResponseMessage response = await httpClient.PostAsync(fullUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
        public async Task<string> Put(string endpoint, string jsonContent)
        {
            if (!IsConnected())
            {
                await App.Current.MainPage.DisplayAlert("Error", "Favor de revisar tu coneccion a internet y vuelvea intentarlo gracias  ", "OK");
                return null;
            }
            string fullUrl = baseUrl + endpoint;
            HttpResponseMessage response = await httpClient.PutAsync(fullUrl, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
