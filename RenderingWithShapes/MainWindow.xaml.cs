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

namespace RenderingWithShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum SelectedShape { Circle, Rectangle, Line };
        private SelectedShape currentShape;
        private bool isFlipped;

        public MainWindow()
        {
            InitializeComponent();
            isFlipped = false;
        }

        private void canvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape resultShape = null;
            switch (currentShape)
            {
                case SelectedShape.Circle:
                    resultShape = new Ellipse() { Height = 35, Width = 35 };
                    RadialGradientBrush brush = new RadialGradientBrush();
                    brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF0017FF"), 0));
                    brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFF50707"), 1));
                    brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFE8FF00"), 0.5));
                    resultShape.Fill = brush;
                    break;
                case SelectedShape.Rectangle:
                    resultShape = new Rectangle() { Fill = Brushes.Red, Height = 35, Width = 35, RadiusX = 10, RadiusY = 10 };
                    break;
                case SelectedShape.Line:
                    resultShape = new Line()
                    {
                        Stroke = Brushes.Blue,
                        StrokeThickness = 10,
                        X1 = 0,
                        X2 = 50,
                        Y1 = 0,
                        Y2 = 50,
                        StrokeStartLineCap = PenLineCap.Triangle,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    break;
                default:
                    return;
            }

            if (isFlipped)
            {
                RotateTransform rotate = new RotateTransform(-180);
                resultShape.RenderTransform = rotate;
            }

            Canvas.SetLeft(resultShape, e.GetPosition(canvasDrawingArea).X); 
            Canvas.SetTop(resultShape, e.GetPosition(canvasDrawingArea).Y);
            canvasDrawingArea.Children.Add(resultShape);
        }

        private void circleOption_Click(object sender, RoutedEventArgs e)
        {
            currentShape = SelectedShape.Circle;
        }

        private void lineOption_Click(object sender, RoutedEventArgs e)
        {
            currentShape = SelectedShape.Line;
        }

        private void rectOption_Click(object sender, RoutedEventArgs e)
        {
            currentShape = SelectedShape.Rectangle;
        }

        private void canvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(sender as Canvas);
            HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);
            if (result != null)
            {
                canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
            }
        }

        private void flipCanvas_Click(object sender, RoutedEventArgs e)
        {
            if (flipCanvas.IsChecked == true)
            {
                RotateTransform rotate = new RotateTransform(-180);
                canvasDrawingArea.LayoutTransform = rotate;
                isFlipped = true;
            }
            else
            {
                canvasDrawingArea.LayoutTransform = null;
                isFlipped = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FunWithTransforms window = new FunWithTransforms();
            window.ShowDialog();
        }
    }
}
