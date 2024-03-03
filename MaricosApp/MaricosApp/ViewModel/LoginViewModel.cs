using MaricosApp.Models;
using MaricosApp.Serivices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MaricosApp.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
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
        public LoginViewModel()
        {
            apiService = new ApiService();
        }
        private async void ShowErrorAlert(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "OK");
        }
        private string Modelo(string email, string password)
        {
            var usuario = new UsuarioRequest
            {
                Email = email,
                Password = password
            };
            return JsonConvert.SerializeObject(usuario);
        }
        private string ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Password cannot be empty.";
            }
            if (password.Length < 8)
            {
                return "Password must be at least 8 characters long.";
            }
            if (!password.Any(char.IsUpper))
            {
                return "Password must contain at least one uppercase letter.";
            }
            if (!password.Any(char.IsLower))
            {
                return "Password must contain at least one lowercase letter.";
            }
            if (!password.Any(char.IsDigit))
            {
                return "La Contrasena Debe de Tener Un Numero";
            }
            if (!password.Any(IsSpecialChar))
            {
                return "Password must contain at least one special character.";
            }
            return string.Empty;
        }
        private bool IsSpecialChar(char ch) => !char.IsLetterOrDigit(ch);
        public async void Login(string email, string password)
        {
            try
            {
                IsBusy = true;
                if (!await IsConnected())
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No hay conexión a Internet", "OK");
                    return;
                }
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Correo y Contrasena son Requeridos", "OK");
                    return;
                }
                string validationResult = ValidatePassword(password);
                if (!string.IsNullOrEmpty(validationResult))
                {
                    ShowErrorAlert("Error", validationResult);
                    return;
                }
                string response = await apiService.Post("/api/user/validar", Modelo(email, password));
                if (response != null)
                {
                    Console.WriteLine("Respuesta del servidor: " + response);
                    await SecureStorage.SetAsync("AccessToken", response);
                    var navigationPage = new NavigationPage(new HomePage1());
                    App.Current.MainPage = navigationPage;
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage1());
                    await App.Current.MainPage.DisplayAlert("Mariscos App", "Inicio de Sesion Correcto", "OK");
                }
                else
                {
                    Console.WriteLine("Error en la solicitud");
                    await App.Current.MainPage.DisplayAlert("Error", "Error en el Inicio de Sesion Favor de Reintentar", "OK");
                } 
            }
            finally
            {
                IsBusy = false; // Oculta el indicador de carga
            }

        }
    }
}
