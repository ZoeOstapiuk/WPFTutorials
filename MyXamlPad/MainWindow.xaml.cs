﻿using System;
using System.IO;
using System.Windows.Markup;
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

namespace MyXamlPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("YourXaml.xaml"))
            {
                txtXamlData.Text = File.ReadAllText("YourXaml.xaml");
            }
            else
            {
                txtXamlData.Text = "<Window xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\n"
                    + " xmlns:x=\"http://schemas.microsoft.com/winfx/200 6/xaml\"\n"
                    + "Height =\"400\" Width =\"500\" WindowStartupLocation=\"CenterScreen\">\n" 
                    + "<StackPanel>\n" + "</StackPanel>\n" + "</Window>";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.WriteAllText("YourXaml.xaml", txtXamlData.Text);
            Application.Current.Shutdown();
        }

        private void btnViewXaml_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("YourXaml.xaml", txtXamlData.Text);
            Window resultWindow = null;

            try
            {
                using (Stream sr = File.Open("YourXaml.xaml", FileMode.Open))
                {
                    resultWindow = (Window)XamlReader.Load(sr);
                    resultWindow.ShowDialog();
                    resultWindow.Close();
                    resultWindow = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMyWindow_Click(object sender, RoutedEventArgs e)
        {
            MyWindow window = new MyWindow();
            window.Show();
        }
    }
}
