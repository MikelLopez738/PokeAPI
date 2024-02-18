using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Application.MVVM.View.Partials
{
    /// <summary>
    /// Lógica de interacción para ProductoPartial.xaml
    /// </summary>
    public partial class ProductoPartial : UserControl
    {
        public ProductoPartial()
        {
            InitializeComponent();
        }

        public ImageSource ProductoSource
        {
            get { return (ImageSource)GetValue(ProductoSourceProperty); }
            set { SetValue(ProductoSourceProperty, value);}
        }

        public static readonly DependencyProperty ProductoSourceProperty = DependencyProperty.Register("ProductoSource", typeof(ImageSource), typeof(ProductoPartial));
    }
}
