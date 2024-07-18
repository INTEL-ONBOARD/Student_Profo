using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using stu_profo.Controller;

namespace stu_profo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool status = false;

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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Trigger binding update
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
                signupScreen.Visibility = Visibility.Visible;
            }
            else
            {
                statusLabel.Content = "Checking failed! Restart the application....";
                signupScreen.Visibility = Visibility.Hidden;
                loadingScreen.Visibility = Visibility.Visible;
                System.Diagnostics.Debug.WriteLine("Connection failed!");
            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            userController userCtn = new userController();
            System.Diagnostics.Debug.WriteLine(emailInput.Text + passwordInput.Text);
            if (userCtn.validateUser(emailInput.Text, passwordInput.Text))
            {
                signupScreen.Visibility = Visibility.Hidden;
                desktop4.Visibility = Visibility.Visible;

                // Set background for desktop4
                MainBackground = Brushes.White;
                MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Group 1000001063.png")));
            }
            else
            {
                signupScreen.Visibility = Visibility.Visible;
                desktop3.Visibility = Visibility.Hidden;

                // Set background for signup screen
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
            System.Diagnostics.Debug.WriteLine("calling");
        }

        private void showinfo_Click(object sender, RoutedEventArgs e)
        {
            desktop4.Visibility = Visibility.Hidden;
            desktop3.Visibility = Visibility.Visible;

            // Set background for desktop3
            MainBackground = Brushes.White;
            MainBackgroundImage = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/View/Group 1000001063.png")));
        }
    }
}
