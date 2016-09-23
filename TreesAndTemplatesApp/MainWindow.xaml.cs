using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace TreesAndTemplatesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder dataToShow;
        private Control ctrlToExamine;

        public MainWindow()
        {
            InitializeComponent();
            
            ctrlToExamine = null;
        }

        private void btnShowLogicalTree_Click(object sender, RoutedEventArgs e)
        {
            dataToShow = new StringBuilder("");
            BuildLogicalTree(0, this);
            this.txtDisplayArea.Text = dataToShow.ToString();
        }

        private void BuildLogicalTree(int depth, object obj)
        {
            dataToShow.Append(new string(' ', depth) + obj.GetType().Name + "\n");
            if (!(obj is DependencyObject))
            {
                return;
            }

            foreach (var item in LogicalTreeHelper.GetChildren(obj as DependencyObject))
            {
                BuildLogicalTree(depth + 5, item);
            }
        }

        private void btnShowVisualTree_Click(object sender, RoutedEventArgs e)
        {
            dataToShow = new StringBuilder("");
            BuildVisualTree(0, this);
            this.txtDisplayArea.Text = dataToShow.ToString();
        }

        private void BuildVisualTree(int depth, DependencyObject obj)
        {
            dataToShow.Append(new string(' ', depth) + obj.GetType().Name + "\n");

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                BuildVisualTree(depth + 2, VisualTreeHelper.GetChild(obj, i));
            }
        }

        private void btnTemplate_Click(object sender, RoutedEventArgs e)
        {
            dataToShow = new StringBuilder("");
            ShowTemplate();
            this.txtDisplayArea.Text = dataToShow.ToString();
        }

        private void ShowTemplate()
        {
            if (ctrlToExamine != null)
            {
                stackTemplatePanel.Children.Remove(ctrlToExamine);
            }

            try
            {
                Assembly asm = Assembly.Load("PresentationFramework, Version=4.0.0.0," + "Culture = neutral, PublicKeyToken = 31bf3856ad364e35");
                ctrlToExamine = (Control)asm.CreateInstance(txtFullName.Text);
                ctrlToExamine.Height = 200;
                ctrlToExamine.Width = 200;
                ctrlToExamine.Margin = new Thickness(5);
                stackTemplatePanel.Children.Add(ctrlToExamine);

                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                XmlWriter xWriter = XmlWriter.Create(dataToShow, xmlSettings);

                XamlWriter.Save(ctrlToExamine.Template, xWriter);
            }
            catch (Exception ex)
            {
                dataToShow = new StringBuilder(ex.Message);
            }
        }
    }
}
