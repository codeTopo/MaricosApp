using MaricosApp.Models;
using MaricosApp.Serivices;
using MaricosApp.View.ProductoPopup;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MaricosApp.ViewModel
{
    public class ConceptoViewModel : BaseViewModel
    {
        private ObservableCollection<ConceptoList> conceptoLista;
        public ObservableCollection<ConceptoList> ConceptoLista
        {
            get { return conceptoLista; }
            set
            {
                if (SetProperty(ref conceptoLista, value))
                {
                    // Puedes agregar lógica adicional si es necesario cuando cambie la propiedad.
                }
            }
        }
        public Command AgregarConceptoCommand => new Command(AgregarConcepto);


        public ConceptoViewModel()
        {
            ConceptoLista = new ObservableCollection<ConceptoList>();   
        }

        public void AgregarConcepto()
        {
            int nuevoContador = ConceptoLista.Count + 1;

            ConceptoList nuevoConcepto = new ConceptoList
            {
                Contador = nuevoContador,
            };

            ConceptoLista.Add(nuevoConcepto);
        }
    }
}
