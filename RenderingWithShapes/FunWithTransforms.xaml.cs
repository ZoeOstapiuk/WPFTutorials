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
using System.Windows.Shapes;

namespace RenderingWithShapes
{
    /// <summary>
    /// Interaction logic for FunWithTransforms.xaml
    /// </summary>
    public partial class FunWithTransforms : Window
    {
        public FunWithTransforms()
        {
            InitializeComponent();
        }

        private void btnSkew_Click(object sender, RoutedEventArgs e)
        {
            myCanvas.LayoutTransform = new SkewTransform(40, -20);
        }

        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            myCanvas.LayoutTransform = new RotateTransform(180);
        }

        private void btnFlip_Click(object sender, RoutedEventArgs e)
        {
            myCanvas.LayoutTransform = new ScaleTransform(-1, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenderingWithVisual w = new RenderingWithVisual();
            w.ShowDialog();
        }
    }
}
