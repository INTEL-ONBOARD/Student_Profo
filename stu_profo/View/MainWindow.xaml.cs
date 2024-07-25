using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public MainWindow()
        {
            InitializeComponent();

            // Dummy data for Courses
            Courses = new ObservableCollection<Course>
            {
                new Course { CourseName = "Diploma in Software Engineering", CourseCode = "DSE231F/CO", CourseID = "CODSE231F-055",GPA = 3.4 },
                new Course { CourseName = "Another Course", CourseCode = "AC123", CourseID = "AC123-001",GPA = 3.4 }
            };

            // Dummy data for Grades
            Grades = new ObservableCollection<GradeItem>
            {
              
                new GradeItem { Grade = "A+", SubjectDetails = "DSE231F/CO/Introduction to Computer Science" },
                new GradeItem { Grade = "A+", SubjectDetails = "DSE231F/CO/Introduction to Computer Science" }
            };

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

        private async void Login(object sender, RoutedEventArgs e)
        {
            userController userCtn = new userController();
            System.Diagnostics.Debug.WriteLine(emailInput.Text + passwordInput.Password);

            if (userCtn.validateUser(emailInput.Text, passwordInput.Password))
            {
                signinScreen.Visibility = Visibility.Hidden;


                List<blockModel> pm = dataController.getProgramms();
                pBox.ItemsSource = pm;
                pBoxC.ItemsSource = pm;
                pBox.DisplayMemberPath = "text";
                pBoxC.DisplayMemberPath = "text";
                pBox.SelectedValuePath = "Value";
                pBoxC.SelectedValuePath = "Value";



                //foreach (blockModel pmModel in pm)
                //{
                //    pBox.Items.Add(pmModel.text);
                //    System.Diagnostics.Debug.WriteLine(pmModel.text + " " + pmModel.value);
                //}
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
            blockModel selectedS = (blockModel)sBoxC.SelectedItem;
            System.Diagnostics.Debug.WriteLine($"{selectedS.value}");
            batch_id = selectedS.value;
            dataController.setProgramm("configStudent.txt", selectedS.value);

            config2.Visibility = Visibility.Hidden;
            home.Visibility = Visibility.Visible;
            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Group 1000001063.png")));
            ShowWarningOverlay(home, ladinoverlay,true);
            await Task.Delay(3000);
            ShowWarningOverlay(home, ladinoverlay, false);

            batchLabel.Content = batch.text;
            studentLabel.Content = student.text;

        }

        private void Signup(object sender, RoutedEventArgs e)
        {
            // Signup code here
            
        }

        private void showinfo_Click(object sender, RoutedEventArgs e)
        {
            home.Visibility = Visibility.Hidden;
            Search.Visibility = Visibility.Visible;

            // Set background for desktop3
            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Home.png")));
        }

        private void search_back(object sender, RoutedEventArgs e)
        {
            // Hide the student view and show the home view
            Search.Visibility = Visibility.Hidden;
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
                blurEffect.Radius = 10;
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
                bBoxC.ItemsSource = bm;
                bBoxC.DisplayMemberPath = "text";
                bBoxC.SelectedValuePath = "Value";
            }
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
    }
}
