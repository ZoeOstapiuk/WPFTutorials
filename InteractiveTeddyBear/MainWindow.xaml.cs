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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InteractiveTeddyBear
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool[] isSpinning;

        public MainWindow()
        {
            InitializeComponent();

            isSpinning = new bool[] { false, false, false, false, false, false, false };
        }

        private byte changeChannel(int x)
        {
            if (x > 255)
            {
                return 255;
            }
            if (x < 0)
            {
                return 0;
            }
            return (byte)x;
        }

        private void Body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path body = sender as Path;

            LinearGradientBrush brush = body.Fill as LinearGradientBrush;
            brush.GradientStops[1] = new GradientStop(colorPicker.SelectedColor.Value, 0.5);

            var darkerPart1 = colorPicker.SelectedColor.Value;
            darkerPart1.R = changeChannel(darkerPart1.R - 25);
            darkerPart1.G = changeChannel(darkerPart1.G - 25);
            darkerPart1.B = changeChannel(darkerPart1.B - 25);

            var darkerPart2 = colorPicker.SelectedColor.Value;
            darkerPart2.R = changeChannel(darkerPart1.R - 35);
            darkerPart2.G = changeChannel(darkerPart1.G - 35);
            darkerPart2.B = changeChannel(darkerPart1.B - 35);

            brush.GradientStops[0] = new GradientStop(darkerPart1, 0);
            brush.GradientStops[2] = new GradientStop(darkerPart2, 1);
        }

        private void Stomach_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path stomach = sender as Path;

            RadialGradientBrush brush = stomach.Fill as RadialGradientBrush;
            brush.GradientStops[0] = new GradientStop(colorPicker.SelectedColor.Value, 0);

            var darkerPart = colorPicker.SelectedColor.Value;
            darkerPart.R = changeChannel(darkerPart.R - 75);
            darkerPart.G = changeChannel(darkerPart.G - 75);
            darkerPart.B = changeChannel(darkerPart.B - 75);

            brush.GradientStops[1] = new GradientStop(darkerPart, 1);
        }

        private void PawsThings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path pawThing = sender as Path;

            LinearGradientBrush brush = pawThing.Fill as LinearGradientBrush;
            brush.GradientStops[0] = new GradientStop(colorPicker.SelectedColor.Value, 0);

            var darkerPart = colorPicker.SelectedColor.Value;
            darkerPart.R = changeChannel(darkerPart.R - 100);
            darkerPart.G = changeChannel(darkerPart.G - 100);
            darkerPart.B = changeChannel(darkerPart.B - 100);

            brush.GradientStops[1] = new GradientStop(darkerPart, 1);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            string content = (sender as TextBlock).Text;
            
            switch (content)
            {
                case "P":
                    doSpin((sender as TextBlock), 0);
                    break;
                case "A":
                    doSpin((sender as TextBlock), 1);
                    break;
                case "I":
                    doSpin((sender as TextBlock), 2);
                    break;
                case "N":
                    doSpin((sender as TextBlock), 3);
                    break;
                case "T":
                    doSpin((sender as TextBlock), 4);
                    break;
                case "M":
                    doSpin((sender as TextBlock), 5);
                    break;
                case "E":
                    doSpin((sender as TextBlock), 6);
                    break;
            }

        }

        private void doSpin(TextBlock block, int num)
        {
            if (!isSpinning[num])
            {
                isSpinning[num] = true;
                
                DoubleAnimation dblAnimFromStart = new DoubleAnimation();
                dblAnimFromStart.From = 0;
                dblAnimFromStart.To = -35;
                dblAnimFromStart.Duration = new Duration(TimeSpan.FromSeconds(0.125));

                DoubleAnimation dblAnimSwing = new DoubleAnimation();
                dblAnimSwing.From = -35;
                dblAnimSwing.To = 35;
                dblAnimSwing.AutoReverse = true;
                dblAnimSwing.RepeatBehavior = new RepeatBehavior(3);
                dblAnimSwing.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                dblAnimSwing.Completed += (o, s) =>
                {
                    isSpinning[num] = false;
                    block.RenderTransform = null;
                };

                RotateTransform rt = new RotateTransform();
                block.RenderTransform = rt;
                rt.BeginAnimation(RotateTransform.AngleProperty, dblAnimFromStart);
                rt.BeginAnimation(RotateTransform.AngleProperty, dblAnimSwing);
            }
        }
    }
}
