using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using stu_profo.Controller;
using stu_profo.Model;
using stu_profo.View;


namespace stu_profo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool status = false;
        private string batch_id = "";
        private string programme_id = "";
        private string student_id = "";
        
        private blockModel batch;
        private blockModel programme;
        private blockModel student;


        public ObservableCollection<GradeItem> Grades { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<ScrollItemModel> Items { get; set; }


        private Brush _mainBackground;
        public Brush MainBackground
        {
            get { return _mainBackground; }
            set
            {
                _mainBackground = value;
                OnPropertyChanged(nameof(MainBackground));
            }
        }

        private ImageBrush _mainBackgroundImage;
        public ImageBrush MainBackgroundImage
        {
            get { return _mainBackgroundImage; }
            set
            {
                _mainBackgroundImage = value;
                OnPropertyChanged(nameof(MainBackgroundImage));
            }
        }

        private object _selectedStudent;
        public object SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyyChanged();
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            // Dummy data for Courses
            /*       Courses = new ObservableCollection<Course>
               {
                   new Course { Id = "1", Subject = "Mathematics", CourseWork = "A", Exam = "B+", FinalGrade = "A-", Points = "4.0" },
                   new Course { Id = "2", Subject = "Physics", CourseWork = "B", Exam = "A", FinalGrade = "A-", Points = "3.7" },
                   new Course { Id = "3", Subject = "Chemistry", CourseWork = "A-", Exam = "A-", FinalGrade = "A-", Points = "3.9" },
                   new Course { Id = "4", Subject = "Biology", CourseWork = "B+", Exam = "B+", FinalGrade = "B+", Points = "3.5" },
                   new Course { Id = "5", Subject = "Computer Science", CourseWork = "A", Exam = "A", FinalGrade = "A", Points = "4.0" }
               };
       */
            Items = new ObservableCollection<ScrollItemModel>
        {
            new ScrollItemModel { Content = "Item 1" },
            new ScrollItemModel { Content = "Item 2" },
            new ScrollItemModel { Content = "Item 3" }
        };
            // Dummy data for Grades

            Courses = new ObservableCollection<Course>();
            DataContext = this;
            // Set default background
            MainBackground = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.5),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#1a4e96"), 0.0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#164381"), 0.7),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#0C3771"), 1.0)
                }
            };

            loadingScreen.Visibility = Visibility.Visible;
        }

        protected void OnPropertyyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ValidateLoad()
        {
            try
            {
                Ping myPing = new Ping();
                string host = "www.nibmworldwide.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                status = false;
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                status = true;
                return false;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Trigger binding update
        }

        private void StViewbackbutton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the student view and show the home view
            Search.Visibility = Visibility.Hidden;
            home.Visibility = Visibility.Visible;

            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));
            // Optionally, reset the background if needed

            //engine.dumpProgrammes();
        }

        private void logoutbtnClick(object sender, RoutedEventArgs e)
        {
            // Hide the student view and show the home view
            home.Visibility = Visibility.Hidden;
            signinScreen.Visibility = Visibility.Visible;
            MainBackground = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.5),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#1a4e96"), 0.0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#164381"), 0.7),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#0C3771"), 1.0)
                }
            };
            MainBackgroundImage = null;
            bBoxC.Text = "";

        }

        private async void cont(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "Checking....";
            statusLabel.Visibility = Visibility.Visible;

            await Task.Delay(1000);

            statusLabel.Content = "Checking dependencies";

            if (ValidateLoad())
            {
                statusLabel.Content = "Checking done";
                System.Diagnostics.Debug.WriteLine("Connection success!");

                await Task.Delay(3000);

                loadingScreen.Visibility = Visibility.Hidden;
                signinScreen.Visibility = Visibility.Visible;
            }
            else
            {
                statusLabel.Content = "Checking failed! Restart the application....";
                signinScreen.Visibility = Visibility.Hidden;
                loadingScreen.Visibility = Visibility.Visible;
                System.Diagnostics.Debug.WriteLine("Connection failed!");
            }
        }

        private void TogglePasswordVisibilityREG(object sender, RoutedEventArgs e)
        {
            if (passwordInputreg.Visibility == Visibility.Visible)
            {
                textInputreg.Text = passwordInputreg.Password;
                passwordInputreg.Visibility = Visibility.Collapsed;
                textInputreg.Visibility = Visibility.Visible;
                ((Button)sender).Content = "🙈"; // Change icon to hidden eye
            }
            else
            {
                passwordInputreg.Password = textInputreg.Text;
                textInputreg.Visibility = Visibility.Collapsed;
                passwordInputreg.Visibility = Visibility.Visible;
                ((Button)sender).Content = "👁"; // Change icon to visible eye

            }
        }

       

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (passwordInput.Visibility == Visibility.Visible)
            {
                textInput.Text = passwordInput.Password;
                passwordInput.Visibility = Visibility.Collapsed;
                textInput.Visibility = Visibility.Visible;
                ((Button)sender).Content = "🙈"; // Change icon to hidden eye
            }
            else
            {
                passwordInput.Password = textInput.Text;
                textInput.Visibility = Visibility.Collapsed;
                passwordInput.Visibility = Visibility.Visible;
                ((Button)sender).Content = "👁"; // Change icon to visible eye
            }
        }

        private void Find_Ideal_Course(object sender, RoutedEventArgs e)
        {
            // Hide the current content
            hbutton.Visibility = Visibility.Collapsed;
            himage.Visibility = Visibility.Collapsed;
            htext1.Visibility = Visibility.Collapsed;
            htext2.Visibility = Visibility.Collapsed;
            Htext3.Visibility = Visibility.Collapsed;
            Hname4.Visibility = Visibility.Collapsed;

            // Show the new ScrollViewer
            scrollViewer.Visibility = Visibility.Visible;
        }
        private void EmailTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            ResetTextBoxes();
        }

        private void PasswordTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            ResetTextBoxes();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            userController userCtn = new userController();
            System.Diagnostics.Debug.WriteLine(emailInput.Text + passwordInput.Password);

            if (userCtn.validateUser(emailInput.Text, passwordInput.Password))
            {
                signinScreen.Visibility = Visibility.Hidden;

                List<blockModel> pm = dataController.getProgramms();
                pBoxSearch.ItemsSource = pm;
                pBoxC.ItemsSource = pm;
                pBoxC.DisplayMemberPath = "text";
                pBoxC.SelectedValuePath = "Value";

                signinScreen.Visibility = Visibility.Hidden;
                config1.Visibility = Visibility.Visible;

                MainBackground = new RadialGradientBrush
                {
                    GradientOrigin = new Point(0.5, 0.5),
                    Center = new Point(0.5, 0.5),
                    RadiusX = 0.5,
                    RadiusY = 0.5,
                    GradientStops = new GradientStopCollection
            {
                new GradientStop((Color)ColorConverter.ConvertFromString("#1a4e96"), 0.0),
                new GradientStop((Color)ColorConverter.ConvertFromString("#164381"), 0.7),
                new GradientStop((Color)ColorConverter.ConvertFromString("#0C3771"), 1.0)
            }
                };
                MainBackgroundImage = null; // Remove the image if applicable

                // Reset text boxes to default style
                ResetTextBoxes();
            }
            else
            {
                ShowWarningOverlay(signinScreen, warningOverlayGrid, true);
                await Task.Delay(3000);
                ShowWarningOverlay(signinScreen, warningOverlayGrid, false);
                // Change the border colors of the text boxes to red
                emailInput.BorderBrush = Brushes.Red;
                passwordInput.BorderBrush = Brushes.Red;
            }
            System.Diagnostics.Debug.WriteLine("calling");
        }

        private void getbatch(object sender, RoutedEventArgs e)
        {
            bBoxC.IsEnabled = true;
            System.Diagnostics.Debug.WriteLine($"{pBoxC.Text}");
        }

        private void ResetTextBoxes()
        {
            emailInput.BorderBrush = Brushes.Gray;
            passwordInput.BorderBrush = Brushes.Gray;
        }


        private async void Donebtn_Click(object sender, RoutedEventArgs e)
        {
            student = (blockModel)sBoxC.SelectedItem;
            System.Diagnostics.Debug.WriteLine($"{student.value}");
            batch_id = student.value;
            dataController.setProgramm("configStudent.txt", student.value);
            Image img = new Image();
            //pfp.ImageSource = new BitmapImage(new Uri("pack://application:,,,/View/p1.png"));

            config2.Visibility = Visibility.Hidden;
            home.Visibility = Visibility.Visible;
            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));
            ShowWarningOverlay(home, ladinoverlay, true);
            await Task.Delay(3000);
            ShowWarningOverlay(home, ladinoverlay, false);




            batchLabel.Content = batch.text;
            studentLabel.Content = student.text;
        }

        private void Signup(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(emailInputreg.Text);
            System.Diagnostics.Debug.WriteLine(passwordInputreg.Password.ToString());
            userController userCtn = new userController();
            if (userCtn.addUser(emailInputreg.Text, passwordInputreg.Password.ToString()))
            {
                System.Diagnostics.Debug.WriteLine("Done");
            }

        }

        private void showinfo_Click(object sender, RoutedEventArgs e)
        {

            pBoxSearch.DisplayMemberPath = "text";
            pBoxSearch.SelectedValuePath = "Value";

            bBoxSearch.DisplayMemberPath = "text";
            bBoxSearch.SelectedValuePath = "Value";

            home.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Visible;



            // Set background for desktop3
            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));


        }

        private void getBatches(object sender, EventArgs e)
        {
            try
            {
                blockModel programmeSelected = (blockModel)pBoxSearch.SelectedItem;
                dataController.setProgramm("config.txt", programmeSelected.value);
                List<blockModel> bm = dataController.getBatches();
                bBoxSearch.ItemsSource = bm;
                bBoxSearch.SelectedValuePath = "Value";
                bBoxSearch.DisplayMemberPath = "text";
            }
            catch(Exception ex) 
            {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
            }


        }

        private void getStudents(object sender, EventArgs e)
        {
            try
            {
                blockModel batchSelected = (blockModel)bBoxSearch.SelectedItem;
                dataController.setProgramm("configBatch.txt", batchSelected.value);
                List<blockModel> sm = dataController.getStudents();
                sBoxSearch.ItemsSource = sm;
                sBoxSearch.SelectedValuePath = "Value";
                sBoxSearch.DisplayMemberPath = "text";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        //Search feature :)
        private async void clickSearch(object sender, RoutedEventArgs e)
        {
            // Show loading GIF and apply blur effect
            loadingGif.Visibility = Visibility.Visible;
            ApplyBlurEffect(Search, true); // Assuming 'mainGrid' is the parent UI element that should be blurred

            line.Visibility = Visibility.Visible;
            leftSector.Visibility = Visibility.Visible;
            viewBoxFrame.Visibility = Visibility.Visible;
            filterBtn1.Visibility = Visibility.Visible;
            filterBtn2.Visibility = Visibility.Visible;
            filterBtn3.Visibility = Visibility.Visible;

            ErrorMessageTextBlock.Visibility = Visibility.Hidden;

            System.Diagnostics.Debug.WriteLine($"{sBoxSearch.Text}");

            student = (blockModel)sBoxSearch.SelectedItem;
            dataController.setStudent("configStudent.txt", student.value);

            // Fetch data asynchronously
            await Task.Run(() => dataController.getStudents());

            customLinkedList dataset = dataController.getStudentsResults();

            batchDataLabel.Text = bBoxSearch.Text;
            studentDataLabel.Text = sBoxSearch.Text;

            Courses.Clear();
            if (dataset != null)
            {
                List<dataModel> dataSet = dataset.DisplayForward();
                if (dataSet != null)
                {
                    foreach (dataModel data in dataSet)
                    {
                        System.Diagnostics.Debug.WriteLine(">>>" + data.FinalGrade + data.Exam + data.CourseWork + data.Subject);
                        Course course = new Course();
                        course.Subject = data.Subject;
                        course.Exam = data.Exam;
                        course.CourseWork = data.CourseWork;
                        course.FinalGrade = data.FinalGrade;
                        course.Points = data.Points;
                        System.Diagnostics.Debug.WriteLine(">>>values" + data.Exam + "  " + data.Exam);
                        Courses.Add(course);
                    }
                    double gpa = gpaCalculator.CalculateGPA(dataSet);
                    System.Diagnostics.Debug.WriteLine(">>>GPA" + gpa);
                    gpaLabel.Text = gpa.ToString();
                    gpaPrecentageLabel.Text = ((gpa / 4.00) * 100).ToString("0.0") + "%";
                }
            }

            // Hide loading GIF and remove blur effect
            loadingGif.Visibility = Visibility.Hidden;
            ApplyBlurEffect(Search, false);
        }

        private void ApplyBlurEffect(UIElement targetPage, bool applyBlur)
        {
            if (targetPage == null)
            {
                throw new ArgumentNullException(nameof(targetPage), "targetPage cannot be null");
            }

            if (applyBlur)
            {
                // Apply blur effect to the target page
                BlurEffect blurEffect = new BlurEffect
                {
                    Radius = 50
                };
                targetPage.Effect = blurEffect;
            }
            else
            {
                // Remove blur effect from the target page
                targetPage.Effect = null;
            }
        }



        private void setting_back(object sender, RoutedEventArgs e)
        {
            // Hide the student view and show the home view
            Settings.Visibility = Visibility.Hidden;
            home.Visibility = Visibility.Visible;

            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));
            // Optionally, reset the background if needed

            //engine.dumpProgrammes();
        }

        private void search_back(object sender, RoutedEventArgs e)
        {
            // Hide the student view and show the home view
            Search.Visibility = Visibility.Hidden;

            line.Visibility = Visibility.Hidden;
            leftSector.Visibility = Visibility.Hidden;
            viewBoxFrame.Visibility = Visibility.Hidden;
            filterBtn1.Visibility = Visibility.Hidden;
            filterBtn2.Visibility = Visibility.Hidden;
            filterBtn3.Visibility = Visibility.Hidden;

            ErrorMessageTextBlock.Visibility = Visibility.Visible;

            home.Visibility = Visibility.Visible;

            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));
            // Optionally, reset the background if needed

            //engine.dumpProgrammes();
        }

        private async void ShowWarningOverlay(UIElement targetPage, Grid warningOverlayGrid, bool applyBlur)
        {
            if (targetPage == null || warningOverlayGrid == null)
            {
                throw new ArgumentNullException("targetPage or warningOverlayGrid cannot be null");
            }

            if (applyBlur)
            {
                // Apply blur effect to the target page
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 50;
                targetPage.Effect = blurEffect;

                // Show the warning overlay
                warningOverlayGrid.Visibility = Visibility.Visible;
            }
            else
            {
                // Remove blur effect from the target page
                targetPage.Effect = null;

                // Hide the warning overlay
                warningOverlayGrid.Visibility = Visibility.Hidden;
            }
        }

        private void HideWarningOverlay(object sender, RoutedEventArgs e)
        {
            // Remove blur effect from signupScreen
            signinScreen.Effect = null;

            // Hide the warning overlay
            warningOverlayGrid.Visibility = Visibility.Hidden;
        }

        private void LoginHyperlink_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to login screen or perform the login action
            signupScreen.Visibility = Visibility.Hidden;
            signinScreen.Visibility = Visibility.Visible;

            // Optionally, reset the background if needed
            MainBackground = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.5),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#1a4e96"), 0.0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#164381"), 0.7),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#0C3771"), 1.0)
                }
            };
            MainBackgroundImage = null; // Remove the image if applicable
        }

        private void RegisterHyperlink_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to registration screen or perform the registration action
            
            signinScreen.Visibility = Visibility.Hidden;
            signupScreen.Visibility = Visibility.Visible;

            // Optionally, reset the background if needed
            MainBackground = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.5),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#1a4e96"), 0.0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#164381"), 0.7),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#0C3771"), 1.0)
                }
            };
            MainBackgroundImage = null; // Remove the image if applicable
        }

        private void initHome(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (home.Visibility == Visibility.Visible) { System.Diagnostics.Debug.WriteLine("init"); }
        }

        private async void loadbatch(object sender, EventArgs e)
        {
            statusText1.Text = "Please wait a moment... We are fetching batches based on your program selection!";
            await Task.Delay(1000);

            System.Diagnostics.Debug.WriteLine($"{pBoxC.Text}");
            if (pBoxC.Text != "") { 

                bBoxC.IsEnabled = true;
                continueBtn.IsEnabled = true;
                statusText1.Text = "Select a Batch to Continue";
                blockModel selectedP = (blockModel)pBoxC.SelectedItem;
                System.Diagnostics.Debug.WriteLine($"{selectedP.value}");
                programme_id = selectedP.value;
                dataController.setProgramm("config.txt", selectedP.value);

                List<blockModel> bm = dataController.getBatches();
                bBoxSearch.ItemsSource = bm;
                bBoxC.ItemsSource = bm;
                bBoxC.DisplayMemberPath = "text";
                bBoxC.SelectedValuePath = "Value";
            }
        }

        private void settingsload(object sender, EventArgs e)
        {
            home.Visibility = Visibility.Hidden;
            Settings.Visibility = Visibility.Visible;

            // Set background for desktop3
            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));
        }

        private async void Continuebtn_Click(object sender, RoutedEventArgs e)
        {
            statusText1.Text = "Please wait....";
            await Task.Delay(1000);

            config1.Visibility = Visibility.Hidden;
            config2.Visibility = Visibility.Visible;

            System.Diagnostics.Debug.WriteLine($"{bBoxC.Text}");
            if (bBoxC.Text != "")
            {
                continueBtn.IsEnabled = true;

                batch = (blockModel)bBoxC.SelectedItem;
                System.Diagnostics.Debug.WriteLine($"{batch.value}");
                batch_id = batch.value;
                dataController.setProgramm("configBatch.txt", batch.value);

                List<blockModel> sm = dataController.getStudents();
                sBoxC.ItemsSource = sm;
                sBoxC.DisplayMemberPath = "text";
                sBoxC.SelectedValuePath = "Value";
            }


            MainBackground = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.5),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#1a4e96"), 0.0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#164381"), 0.7),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#0C3771"), 1.0)
                }
            };
            MainBackgroundImage = null; // Remove the image if applicable

        }

        private void Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //filter feature :)
        private void filter1(object sender, RoutedEventArgs e)
        {

            this.clickSearch();
            //customLinkedList dataset = dataController.getStudentsResults();
            //Courses.Clear();
            //if (dataset != null)
            //{
            //    List<dataModel> dataSet = dataset.DisplayForward();

            //    dataSet = engineSort.BubbleSortAscending(dataSet, "Points");
            //    if (dataSet != null)
            //    {
            //        foreach (dataModel data in dataSet)
            //        {
            //            System.Diagnostics.Debug.WriteLine(">>>" + data.FinalGrade + data.Exam + data.CourseWork + data.Subject);
            //            Course course = new Course();
            //            course.Subject = data.Subject;
            //            course.Exam = data.Exam;
            //            course.CourseWork = data.CourseWork;
            //            course.FinalGrade = data.FinalGrade;
            //            course.Points = data.Points;

            //            Courses.Add(course);
            //        }
            //    }
            //}
        }


        private void filter2(object sender, RoutedEventArgs e)
        {
            customLinkedList dataset = dataController.getStudentsResults();
            Courses.Clear();
            if (dataset != null)
            {
                List<dataModel> dataSet = dataset.DisplayForward();

                dataSet = engineSort.BubbleSortDescending(dataSet, "Points");
                if (dataSet != null)
                {
                    foreach (dataModel data in dataSet)
                    {
                        System.Diagnostics.Debug.WriteLine(">>>" + data.FinalGrade + data.Exam + data.CourseWork + data.Subject);
                        Course course = new Course();
                        course.Subject = data.Subject;
                        course.Exam = data.Exam;
                        course.CourseWork = data.CourseWork;
                        course.FinalGrade = data.FinalGrade;
                        course.Points = data.Points;

                        Courses.Add(course);
                    }
                }
            }
        }


        private void filter3(object sender, RoutedEventArgs e)
        {
            customLinkedList dataset = dataController.getStudentsResults();
            Courses.Clear();
            if (dataset != null)
            {
                List<dataModel> dataSet = dataset.DisplayForward();

                dataSet = engineSort.BubbleSortAscending(dataSet, "Points");
                if (dataSet != null)
                {
                    foreach (dataModel data in dataSet)
                    {
                        System.Diagnostics.Debug.WriteLine(">>>" + data.FinalGrade + data.Exam + data.CourseWork + data.Subject);
                        Course course = new Course();
                        course.Subject = data.Subject;
                        course.Exam = data.Exam;
                        course.CourseWork = data.CourseWork;
                        course.FinalGrade = data.FinalGrade;
                        course.Points = data.Points;

                        Courses.Add(course);
                    }
                }
            }
        }


        private void OnBackClick(object sender, RoutedEventArgs e)
        {
        }
        private void OnFilterClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
