using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Application.MVVM.ViewModel;

namespace WPF.Application.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para DetallePokemonView.xaml
    /// </summary>
    public partial class DetallePokemonView : UserControl
    {
        public DetallePokemonView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DetallePokemonViewModel viewModel = this.DataContext as DetallePokemonViewModel;
            if (viewModel != null)
            {
                viewModel.CargarDatos(); // Reemplaza "TuFuncion" con el nombre de tu función.
            }
        }
    }
}
