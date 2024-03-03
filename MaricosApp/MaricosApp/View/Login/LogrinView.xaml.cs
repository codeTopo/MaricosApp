using MaricosApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogrinView : ContentPage
    {
        private LoginViewModel viewModel;
        public bool IsPasswordHidden { get; set; } =true;
        public bool IsActivityIndicatorVisible { get; set; } = false;
        public LogrinView()
        {
            InitializeComponent();
            viewModel = new LoginViewModel();
            BindingContext = this;
            IsPasswordHidden = !IsPasswordHidden;
            PasswordEntry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPasswordHidden)));
        }

        private void LoginButton(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            viewModel.Login(email, password);
        }
        private void OnToggleSwitch(object sender, ToggledEventArgs e)
        {
            IsPasswordHidden = !e.Value;

            // Cambiar la visibilidad de la contraseña en el Entry
            PasswordEntry.IsPassword = IsPasswordHidden;
        }

    }
}