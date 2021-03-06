﻿using System;
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
using System.Windows.Media.Animation;

namespace AnimationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSpinning;

        public MainWindow()
        {
            InitializeComponent();
            isSpinning = false;
        }

        private void btnSpinner_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isSpinning)
            {
                isSpinning = true;

                DoubleAnimation dblAnim = new DoubleAnimation();
                dblAnim.Completed += (o, s) => isSpinning = false;

                dblAnim.From = 0;
                dblAnim.To = 360;
                dblAnim.Duration = new Duration(TimeSpan.FromSeconds(4));

                RotateTransform rt = new RotateTransform();
                this.btnSpinner.RenderTransform = rt;

                rt.BeginAnimation(RotateTransform.AngleProperty, dblAnim);
            }
        }

        private void btnSpinner_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation dblAnim = new DoubleAnimation();
            dblAnim.From = 1.0;
            dblAnim.To = 0.0;
            dblAnim.AutoReverse = true;
            btnSpinner.BeginAnimation(Button.OpacityProperty, dblAnim);
        }
    }
}
