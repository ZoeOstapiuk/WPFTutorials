using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppAllCode
{
    public class MainWindow : Window
    {
        private Button btnExitApp;

        public MainWindow(string title, int height, int width)
        {
            // First settings
            this.Title = title;
            this.Height = height;
            this.Width = width;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // If prop. Cancel in EventArgs is true, the window 
            // won't close, otherwise it raises the Closed event
            this.Closing += MainWindow_Closing;
            this.Closed += MainWindow_Closed;

            // Mouse moves settings
            this.MouseMove += MainWindow_MouseMove;

            // Key related event settings
            this.KeyDown += MainWindow_KeyDown;

            // Add the exit button
            btnExitApp = new Button();
            btnExitApp.Click += btnExitApp_Clicked;
            btnExitApp.Content = "Exit Application";
            btnExitApp.Height = 25;
            btnExitApp.Width = 100;
            this.AddChild(btnExitApp);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            btnExitApp.Content = e.Key.ToString();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            this.Title = e.GetPosition(this).ToString();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("See ya!");
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Do you want to close?";
            MessageBoxResult result = MessageBox.Show(msg, "MyApp", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnExitApp_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool)Application.Current.Properties["GodMode"])
            {
                MessageBox.Show("Cheater!!!");
            }
            this.Close();
        }
    }
}
