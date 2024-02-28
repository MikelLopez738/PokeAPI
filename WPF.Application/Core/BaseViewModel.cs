using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF.Application.Core
{
    public  abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public string _nombrePokemon;

        [ObservableProperty]
        public int _elementosPaginado;

        public BaseViewModel()
        {
            ElementosPaginado = 30;
        }

        public virtual void SetParameters(object parameters)
        {
            if (parameters is string parametroString)
            {
                // Asignar el valor a la propiedad en el ViewModel
                NombrePokemon = parametroString;
            }
        }
    }
}
