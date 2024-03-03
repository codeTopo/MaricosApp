using MaricosApp.View.Login;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var accessToken = SecureStorage.GetAsync("AccessToken").Result;
            if (!string.IsNullOrEmpty(accessToken))
            {
                MainPage = new NavigationPage(new HomePage1());
            }
            else
            {
                MainPage = new LogrinView();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
