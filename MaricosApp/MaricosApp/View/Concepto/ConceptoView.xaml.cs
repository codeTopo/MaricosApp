using MaricosApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaricosApp.View.Concepto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConceptoView : ContentPage
    {
        public ConceptoView()
        {
            InitializeComponent();
            BindingContext = new ConceptoViewModel();
        }
    }
}