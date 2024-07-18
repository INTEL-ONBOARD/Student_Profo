using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using static System.Net.WebRequestMethods;
using System.Data;
using stu_profo.Controller;

namespace stu_profo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool status = false;  
        public MainWindow()
        {
            InitializeComponent();
            loadingScreen.Visibility = Visibility.Visible;
            //validateLoad();


        }
        public Boolean validateLoad() {
            try
            {

                Ping myPing = new Ping();
                String host = "www.nibmworldwide.com";
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
            if (validateLoad())
            {
                statusLabel.Content = "Checking done";
            

                System.Diagnostics.Debug.WriteLine("Connection success!");

                await Task.Delay(3000);

                loadingScreen.Visibility = Visibility.Hidden;
                signupScreen.Visibility = Visibility.Visible;
            }
            else
            {
                statusLabel.Content = "Checking faild! Restart the application....";
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
                //blurScreen.Visibility = Visibility.Hidden;
                signupScreen.Visibility = Visibility.Hidden;
                desktop4.Visibility = Visibility.Visible;
            }
            else
            {
                signupScreen.Visibility = Visibility.Visible;
                desktop3.Visibility = Visibility.Hidden;
                //blurScreen.Visibility = Visibility.Visible;
            }
            System.Diagnostics.Debug.WriteLine("calling");

        }

        private void showinfo_Click(object sender, RoutedEventArgs e)
        {
            desktop4.Visibility = Visibility.Hidden;
            desktop3.Visibility = Visibility.Visible;
        }
    }
}