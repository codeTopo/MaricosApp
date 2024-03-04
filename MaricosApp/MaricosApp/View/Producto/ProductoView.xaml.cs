using MaricosApp.Models;
using MaricosApp.View.Concepto;
using MaricosApp.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.ProductoPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoView : ContentView
    {
    
        public ProductoView()
        {
            InitializeComponent();
            BindingContext = new ProductosViewModel();
           
        }

    }
}