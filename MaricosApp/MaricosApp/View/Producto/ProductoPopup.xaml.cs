using MaricosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Producto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductoPopup : Rg.Plugins.Popup.Pages.PopupPage
	{
		public ProductoPopup ()
		{
			InitializeComponent ();
		}
	}
}