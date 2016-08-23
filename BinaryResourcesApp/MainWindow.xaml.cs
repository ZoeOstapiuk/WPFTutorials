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

namespace BinaryAndLogicalResourcesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BitmapImage> images;
        private int currImage;
        private const int MAX_IMAGES = 2;

        public MainWindow()
        {
            InitializeComponent();

            images = new List<BitmapImage>();
            currImage = 0;
        }

        private void btnPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (--currImage < 0)
            {
                currImage = MAX_IMAGES;
            }
            imageHolder.Source = images[currImage];
        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            if (++currImage > MAX_IMAGES)
            {
                currImage = 0;
            }
            imageHolder.Source = images[currImage];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory;

                images.Add(new BitmapImage(new Uri(String.Format(@"{0}\Images\RedHouse.JPG", path))));
                images.Add(new BitmapImage(new Uri(String.Format(@"{0}\Images\PatrolStation.JPG", path))));
                images.Add(new BitmapImage(new Uri(String.Format(@"{0}\Images\Field.JPG", path))));

                this.imageHolder.Source = images[currImage];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            TestWindow w = new TestWindow();
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.ShowDialog();
        }
    }
}
