using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Annotations.Storage;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlsAndAPIs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateDocument();
            AnableAnnotations();
            SetBindings();
            ConfigureGrid();

            // Event handlers for documents (XML Paper Specification)
            btnSaveDoc.Click += (o, e) =>
            {
                using (FileStream fStream = File.Open("documentData.xaml", FileMode.Create))
                {
                    XamlWriter.Save(this.myDocumentReader.Document, fStream);
                }
            };
            btnLoadDoc.Click += (o, s) =>
            {
                using (FileStream fStream = File.Open("documentData.xaml", FileMode.Open))
                {
                    try
                    {
                        FlowDocument doc = XamlReader.Load(fStream) as FlowDocument;
                        this.myDocumentReader.Document = doc;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Loading Doc!");
                    }
                }
            };
        }

        private void ConfigureGrid()
        {
            this.gridInventory.ItemsSource = new List<object>
            {
                new {id = 10, petName = "Zoe"},
                new {id = 12, petName = "Roman"}
            };
        }

        private void SetBindings()
        {
            Binding b = new Binding();
            b.Converter = new MyDoubleConverter();
            b.Source = this.mySB;
            b.Path = new PropertyPath("Value");

            this.lblSBThumb.SetBinding(Label.ContentProperty, b);
        }

        private void AnableAnnotations()
        {
            AnnotationService anoSvc = new AnnotationService(myDocumentReader);
            MemoryStream anoStream = new MemoryStream();
            AnnotationStore store = new XmlStreamStore(anoStream);
            anoSvc.Enable(store);
        }

        private void PopulateDocument()
        {
            this.listOfFunFacts.FontSize = 14;
            this.listOfFunFacts.MarkerStyle = TextMarkerStyle.Circle;
            this.listOfFunFacts.ListItems.Add(new ListItem(new Paragraph(new Run("Fixed documents are for WYSIWYG print ready docs!"))));
            this.listOfFunFacts.ListItems.Add(new ListItem(new Paragraph(new Run("The API supports tables and embedded figures!"))));
            this.listOfFunFacts.ListItems.Add(new ListItem(new Paragraph(new Run("Flow documents are read only!"))));
            this.listOfFunFacts.ListItems.Add(new ListItem(new Paragraph(new Run("BlockUIContainer allows you to embed WPF controls in the document!"))));

            Run prefix = new Run("This paragraph was generated!");
            Bold b = new Bold();
            Run infix = new Run(" ...Dynamically");
            infix.Foreground = Brushes.Red;
            infix.FontSize = 30;
            b.Inlines.Add(infix);
            Run suffix = new Run(" at run time!");

            this.paraBodyText.Inlines.Add(prefix);
            this.paraBodyText.Inlines.Add(infix);
            this.paraBodyText.Inlines.Add(suffix);
        }

        private void RadioButtonClicked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content.ToString())
            {
                case "Ink Mode!":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;

                case "Erase Mode!":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;

                case "Select Mode!":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;

                default:
                    break;
            }
        }

        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (this.comboColors.SelectedIndex)
                {
                    case 0:
                        this.myInkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(255, 0, 0);
                        break;

                    case 1:
                        this.myInkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(0, 255, 0);
                        break;

                    case 2:
                        this.myInkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(0, 0, 255);
                        break;
                }
            }
            catch 
            {
                // At start problems with assigning color to colot attribute
            }
        }

        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Pic Files |*.bin";

            if (openDlg.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fs = new FileStream(openDlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        StrokeCollection strokes = new StrokeCollection(fs);
                        this.myInkCanvas.Strokes = strokes;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Pic Files |*.bin";

            if (saveDlg.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fs = new FileStream(saveDlg.FileName, FileMode.OpenOrCreate))
                    {
                        this.myInkCanvas.Strokes.Save(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.myInkCanvas.Strokes.Clear();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
