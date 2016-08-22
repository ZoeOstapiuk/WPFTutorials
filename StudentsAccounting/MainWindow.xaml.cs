using Microsoft.Win32;
using StudentsAccounting;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace StudentsAccounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TITLE = "Students Accounting";
        private const int MAX_UNDOREDO = 10;
        private UndoRedoContainer<IStudentRepository> undoRedoContainer;
        private CurrentFileStateInfo fileState;

        public MainWindow()
        {
            InitializeComponent();

            fileState = null;
            undoRedoContainer = new UndoRedoContainer<IStudentRepository>(MAX_UNDOREDO);

            this.btnSort.IsEnabled = false;
            this.btnSave.IsEnabled = false;
            this.btnSaveAs.IsEnabled = false;
            this.btnUndo.IsEnabled = false;
            this.btnRedo.IsEnabled = false;
            this.btnAddRecord.IsEnabled = false;
            this.btnEditRecord.IsEnabled = false;
            this.btnDeleteRecord.IsEnabled = false;
            this.btnClear.IsEnabled = false;
            this.btnReverse.IsEnabled = false;
            this.btnSelect.IsEnabled = false;

            List<string> gradesOptions = (new StudentsInfoWindow(new Student())).AvailableGrades().ToList();
            gradesOptions.Insert(0, " — ");
            this.comboExam1.ItemsSource = gradesOptions;
            this.comboExam2.ItemsSource = gradesOptions;
            this.comboExam3.ItemsSource = gradesOptions;
            this.comboExam4.ItemsSource = gradesOptions;
            this.comboExam5.ItemsSource = gradesOptions;
            this.comboExam6.ItemsSource = gradesOptions;

            this.radioGrades.IsChecked = true;
            ChangeRatingGroup(false);
        }

        #region Menu Buttons
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Some specific file is being edited 
            if (fileState != null && fileState.IsChangedAfterLastSaving)
            {
                if (fileState.FileName != null)
                {
                    try
                    {
                        fileState.StudentsRepo.SaveXml(fileState.FileName);

                        // Cancel all redoes and undoes?
                        undoRedoContainer.Reset();
                        AddRepoBackupCopy();

                        fileState.IsChangedAfterLastSaving = false;

                        this.btnUndo.IsEnabled = false;
                        this.btnRedo.IsEnabled = false;

                        this.path.Text = fileState.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, TITLE);
                    }  
                }
                else
                {
                    btnSaveAs_Click(sender, e);
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (CanRelease())
            {
                fileState = new CurrentFileStateInfo
                {
                    FileName = null,
                    IsChangedAfterLastSaving = false,
                    StudentsRepo = new StudentRepository()
                };
                this.dataGridStudents.ItemsSource = fileState.StudentsRepo;
                this.Title = TITLE;

                undoRedoContainer.Reset();
                AddRepoBackupCopy();
                fileState.IsChangedAfterLastSaving = false;

                this.btnUndo.IsEnabled = false;
                this.btnRedo.IsEnabled = false;
                this.btnSave.IsEnabled = true;
                this.btnSaveAs.IsEnabled = true;
                this.btnAddRecord.IsEnabled = true;
                this.btnClear.IsEnabled = true;
                this.btnSort.IsEnabled = true;
                this.btnReverse.IsEnabled = true;
                this.btnSelect.IsEnabled = true;
            }
        }

        private void btnNew_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Create new students list";
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Ready";
        }

        private void btnOpenFromXml_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Open XML-file with students data";
        }

        private void btnOpenFromXml_Click(object sender, RoutedEventArgs e)
        {
            // TODO: make unavailable buttons disabled ("Save/Save As..." where there is nothing to save)
            if (CanRelease())
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                openDlg.DefaultExt = "xml";
                openDlg.Filter = "XML-files |*.xml";

                if (true == openDlg.ShowDialog())
                {
                    fileState = new CurrentFileStateInfo
                    {
                        FileName = openDlg.FileName,
                        StudentsRepo = new StudentRepository()
                    };

                    try
                    {
                        fileState.StudentsRepo.LoadXml(fileState.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, TITLE);
                    }

                    undoRedoContainer.Reset();
                    AddRepoBackupCopy();
                    fileState.IsChangedAfterLastSaving = false;
                    UpdateDataGridFromFileState();

                    this.path.Text = fileState.FileName;

                    this.btnUndo.IsEnabled = false;
                    this.btnRedo.IsEnabled = false;
                    this.btnSave.IsEnabled = true;
                    this.btnSaveAs.IsEnabled = true;
                    this.btnAddRecord.IsEnabled = true;
                    this.btnClear.IsEnabled = true;
                    this.btnSort.IsEnabled = true;
                    this.btnReverse.IsEnabled = true;
                    this.btnSelect.IsEnabled = true;
                }
            }
        }

        private void btnOpenFromTxt_Click(object sender, RoutedEventArgs e)
        {
            // Cancel all redoes and undoes?
            // undoRedoContainer.Reset();
        }

        private void btnOpenFromTxt_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Load data from txt-file";
        }

        private void btnSave_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Save changes made to file or create new file with data";
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Filter = "Xml-files |*.xml";
                saveDlg.DefaultExt = "xml";

                if (true == saveDlg.ShowDialog())
                {
                    if (saveDlg.FileName.Substring(saveDlg.FileName.LastIndexOf('.')) == ".txt")
                    {
                        MessageBox.Show("Not supported in this version", TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    // The best choice
                    if (saveDlg.FileName.Substring(saveDlg.FileName.LastIndexOf('.')) == ".xml")
                    {
                        fileState.StudentsRepo.SaveXml(saveDlg.FileName);
                        fileState.FileName = saveDlg.FileName;

                        // Cancel all redoes and undoes?
                        undoRedoContainer.Reset();
                        AddRepoBackupCopy();

                        fileState.IsChangedAfterLastSaving = false;

                        this.btnUndo.IsEnabled = false;
                        this.btnRedo.IsEnabled = false;
                    }

                    this.path.Text = fileState.FileName;
                } 
            }
        }

        private void btnSaveAs_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Save file";
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Close the application";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUndo_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Step back";
        }

        private void btnRedo_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Step forward";
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (undoRedoContainer.UndoContainerCount != 0)
            {
                this.dataGridStudents.ItemsSource = undoRedoContainer.ItemBackward;
                if (undoRedoContainer.UndoContainerCount == 1)
                {
                    this.btnUndo.IsEnabled = false;
                }

                this.btnRedo.IsEnabled = true;
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (undoRedoContainer.RedoContainerCount != 0)
            {
                this.dataGridStudents.ItemsSource = undoRedoContainer.ItemForward;
                if (undoRedoContainer.RedoContainerCount == 0)
                {
                    this.btnRedo.IsEnabled = false;
                }

                this.btnUndo.IsEnabled = true;
            }
        }

        private void Sort_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Sort the list";
        }

        private void btnSortAge_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                fileState.StudentsRepo.SortByAge();
                UpdateDataGridFromFileState();
                AddRepoBackupCopy();
            }
        }

        private void btnSortRating_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                fileState.StudentsRepo.SortByRating();
                UpdateDataGridFromFileState();
                AddRepoBackupCopy();
            }
        }

        private void btnSortFullName_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                fileState.StudentsRepo.SortByName();
                UpdateDataGridFromFileState();
                AddRepoBackupCopy();
            }
        }

        private void btnReverse_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Reverse the order in your list";
        }

        private void btnReverse_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                fileState.StudentsRepo.Reverse();
                UpdateDataGridFromFileState();
                AddRepoBackupCopy();
            }
        }

        private void btnSelect_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Select students using filter";
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                var result = from s in fileState.StudentsRepo select s;
                if (!String.IsNullOrWhiteSpace(this.filterFName.Text))
                {
                    result = from s in result
                             where s.FirstName == this.filterFName.Text
                             select s;
                }

                if (result.Count() != 0 && !String.IsNullOrWhiteSpace(this.filterLName.Text))
                {
                    result = from s in result
                             where s.LastName == this.filterLName.Text
                             select s;
                }

                string[] mdy = this.bdFilter.Text.Split('/');
                if (result.Count() != 0 && mdy.Length == 3)
                {
                    DateTime date = new DateTime(Convert.ToInt32(mdy[2]), Convert.ToInt32(mdy[0]), Convert.ToInt32(mdy[1]));
                    result = result.Where(s =>
                    {
                        if (this.comboFilterDate.SelectedIndex == 2)
                        {
                            return s.BirthDate.CompareTo(date) > 0;
                        }
                        else if (this.comboFilterDate.SelectedIndex == 1)
                        {
                            return s.BirthDate.CompareTo(date) < 0;
                        }
                        else
                        {
                            return s.BirthDate.CompareTo(date) == 0;
                        }
                    });
                }

                if (result.Count() != 0 && this.radioGrades.IsChecked == true)
                {
                    if (result.Count() != 0 && this.comboExam1.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterEx1.SelectedIndex == 1)
                            {
                                if (this.comboExam1.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam1) > Convert.ToInt32(comboExam1.SelectedItem.ToString()); 
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterEx1.SelectedIndex == 2)
                            {
                                if (this.comboExam1.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam1) < Convert.ToInt32(comboExam1.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.Exam1 == comboFilterEx1.SelectedItem.ToString();
                            }
                        }); 
                    }

                    if (result.Count() != 0 && this.comboExam2.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterEx2.SelectedIndex == 1)
                            {
                                if (this.comboExam2.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam2) > Convert.ToInt32(comboExam2.SelectedItem.ToString());
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterEx2.SelectedIndex == 2)
                            {
                                if (this.comboExam2.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam2) < Convert.ToInt32(comboExam2.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.Exam2 == comboFilterEx2.SelectedItem.ToString();
                            }
                        });
                    }

                    if (result.Count() != 0 && this.comboExam3.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterEx3.SelectedIndex == 1)
                            {
                                if (this.comboExam3.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam3) > Convert.ToInt32(comboExam3.SelectedItem.ToString());
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterEx3.SelectedIndex == 2)
                            {
                                if (this.comboExam3.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam3) < Convert.ToInt32(comboExam3.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.Exam3 == comboFilterEx3.SelectedItem.ToString();
                            }
                        });
                    }

                    if (result.Count() != 0 && this.comboExam4.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterEx4.SelectedIndex == 1)
                            {
                                if (this.comboExam4.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam4) > Convert.ToInt32(comboExam4.SelectedItem.ToString());
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterEx4.SelectedIndex == 2)
                            {
                                if (this.comboExam4.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam4) < Convert.ToInt32(comboExam4.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.Exam4 == comboFilterEx4.SelectedItem.ToString();
                            }
                        });
                    }

                    if (result.Count() != 0 && this.comboExam5.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterEx5.SelectedIndex == 1)
                            {
                                if (this.comboExam5.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam5) > Convert.ToInt32(comboExam5.SelectedItem.ToString());
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterEx5.SelectedIndex == 2)
                            {
                                if (this.comboExam5.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam5) < Convert.ToInt32(comboExam5.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.Exam5 == comboFilterEx5.SelectedItem.ToString();
                            }
                        });
                    }

                    if (this.comboExam6.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterEx6.SelectedIndex == 1)
                            {
                                if (this.comboExam6.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam6) > Convert.ToInt32(comboExam6.SelectedItem.ToString());
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterEx6.SelectedIndex == 2)
                            {
                                if (this.comboExam6.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.Exam6) < Convert.ToInt32(comboExam6.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.Exam6 == comboFilterEx6.SelectedItem.ToString();
                            }
                        });
                    }
                }
                else if (result.Count() != 0 && this.radioRating.IsChecked == true)
                {
                    if (result.Count() != 0 && this.comboRating.SelectedIndex != 0)
                    {
                        result = result.Where(s =>
                        {
                            if (this.comboFilterRating.SelectedIndex == 1)
                            {
                                if (this.comboRating.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.RatingGrade) > Convert.ToInt32(comboRating.SelectedItem.ToString());
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else if (this.comboFilterRating.SelectedIndex == 2)
                            {
                                if (this.comboRating.SelectedIndex != 1)
                                {
                                    return Convert.ToInt32(s.RatingGrade) < Convert.ToInt32(comboRating.SelectedItem.ToString());
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return s.RatingGrade == comboFilterRating.SelectedItem.ToString();
                            }
                        });
                    }
                }

                // Generating result response
                if (result.Count() != 0)
                {
                    this.expanderFilter.IsExpanded = false;

                    this.dataGridFiltered.Visibility = Visibility.Visible;
                    this.dataGridFiltered.ItemsSource = result;
                    this.dataGridFiltered.Items.Refresh();
                }
            }
        }

        private void btnViewHelp_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "View Help and instructions";
        }

        private void btnViewHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbout_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "About";
        }
        #endregion
 
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CanRelease())
            {
                e.Cancel = true;
            }
        }

        private bool CanRelease()
        {
            if (fileState != null && fileState.IsChangedAfterLastSaving)
            {
                var result = MessageBox.Show("Save changes?", TITLE, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result != MessageBoxResult.Cancel && result != MessageBoxResult.None)
                {
                    // Arguments in methods are not used, therefore we can use any object to call the method
                    if (result == MessageBoxResult.Yes)
                    {
                        if (fileState.FileName == null)
                        {
                            btnSaveAs_Click(new object(), new RoutedEventArgs());
                        }
                        else
                        {
                            btnSave_Click(new object(), new RoutedEventArgs());
                        } 
                    }
                    return true;
                }
                return false;
            }
            return true;
        }

        private void btnEditInformation_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Edit selected student information";
        }

        private void btnDeleteRecord_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Delete selected student information from list";
        }

        private void btnbtnDeleteAll_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Clear the list";
        }

        private void btnEditInformation_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGridStudents.SelectedIndex >= 0)
            {
                StudentsInfoWindow studInfo = new StudentsInfoWindow(fileState.StudentsRepo[dataGridStudents.SelectedIndex]);
                
                if (studInfo.ShowDialog() == true)
                {
                    fileState.StudentsRepo[dataGridStudents.SelectedIndex] = studInfo.PassedStudentValue;
                    UpdateDataGridFromFileState();
                    AddRepoBackupCopy();
                }
            }
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null && this.dataGridStudents.SelectedIndex >= 0)
            {
                fileState.StudentsRepo.RemoveStudent(this.dataGridStudents.SelectedIndex);
                UpdateDataGridFromFileState();
                AddRepoBackupCopy();
            }
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                fileState.StudentsRepo.Clear();
                UpdateDataGridFromFileState();
                AddRepoBackupCopy();
            }
        }

        private void AddRepoBackupCopy()
        {
            undoRedoContainer.AddItem((IStudentRepository)fileState.StudentsRepo.Clone());
            fileState.IsChangedAfterLastSaving = true;

            this.btnUndo.IsEnabled = true;
            this.btnRedo.IsEnabled = false;
        }

        private void UpdateDataGridFromFileState()
        {
            this.dataGridStudents.ItemsSource = fileState.StudentsRepo;
            this.dataGridStudents.Items.Refresh();
            this.dataGridStudents.SelectedIndex = -1;
        }

        private void btnAddRecord_MouseEnter(object sender, MouseEventArgs e)
        {
            this.statBarText.Text = "Add a new student to the file";
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            if (fileState != null)
            {
                Student stud = new Student();
                StudentsInfoWindow studWindow = new StudentsInfoWindow(stud);

                if (studWindow.ShowDialog() == true)
                {
                    stud = studWindow.PassedStudentValue;
                    fileState.StudentsRepo.AddStudent(stud);
                    UpdateDataGridFromFileState();
                    AddRepoBackupCopy();
                }
            }
        }

        private void dataGridStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.btnDeleteRecord.IsEnabled = true;
            this.btnEditRecord.IsEnabled = true;
        }

        private void focus_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnDeleteRecord.IsEnabled = false;
            this.btnEditRecord.IsEnabled = false;
        }

        private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MenuItem cur = sender as MenuItem;
            switch (cur.Name)
            {
                case "btnSave":
                    (this.Resources["SaveIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copySaveIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnSaveAs":
                    (this.Resources["SaveAsIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copySaveAsIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnUndo":
                    (this.Resources["UndoIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copyUndoIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnRedo":
                    (this.Resources["RedoIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copyRedoIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnAddRecord":
                    (this.Resources["AddIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copyAddIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnEditRecord":
                    (this.Resources["EditIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copyEditIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnDeleteRecord":
                    (this.Resources["RemoveIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copyRemoveIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnClear":
                    (this.Resources["ClearIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnSort":
                    (this.Resources["SortIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    this.copySortIcon.Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnReverse":
                    (this.Resources["ReverseIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
                case "btnSelect":
                    (this.Resources["SelectIcon"] as System.Windows.Shapes.Path).Fill = cur.IsEnabled ? Brushes.Black : Brushes.Gray;
                    break;
            }
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            switch(this.comboSort.SelectedIndex)
            {
                case 0:
                    btnSortAge_Click(sender, e);
                    break;
                case 1:
                    btnSortRating_Click(sender, e);
                    break;
                case 2:
                    btnSortRating_Click(sender, e);
                    break;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Content.ToString() == "Grades")
            {
                ChangeRatingGroup(false);
                ChangeGradesGroup(true);
            }
            else if (radio.Content.ToString() == "Rating")
            {
                ChangeRatingGroup(true);
                ChangeGradesGroup(false);
            }
        }

        private void ChangeGradesGroup(bool enable)
        {
            this.comboFilterEx1.IsEnabled = enable;
            this.comboFilterEx2.IsEnabled = enable;
            this.comboFilterEx3.IsEnabled = enable;
            this.comboFilterEx4.IsEnabled = enable;
            this.comboFilterEx5.IsEnabled = enable;
            this.comboFilterEx6.IsEnabled = enable;
            this.comboExam1.IsEnabled = enable;
            this.comboExam2.IsEnabled = enable;
            this.comboExam3.IsEnabled = enable;
            this.comboExam4.IsEnabled = enable;
            this.comboExam5.IsEnabled = enable;
            this.comboExam6.IsEnabled = enable;
        }

        private void ChangeRatingGroup(bool enable)
        {
            this.comboFilterRating.IsEnabled = enable;
            this.comboRating.IsEnabled = enable;
        }
    }
}
