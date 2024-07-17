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


namespace stu_profo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadingScreen.Visibility = Visibility.Visible;
            
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
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is your message.");
            Console.WriteLine("hwelo");
        }

        private void callValidate(object sender, RoutedEventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            while (true)
            {
                if (validateLoad())
                {
                    System.Diagnostics.Debug.WriteLine("Conection success!");
                    System.Threading.Thread.Sleep(3000);
                    loadingScreen.Visibility = Visibility.Hidden;
                    signupScreen.Visibility = Visibility.Visible;
                    break;
                }
                else
                {
                    signupScreen.Visibility = Visibility.Visible;
                    System.Diagnostics.Debug.WriteLine("Conection faild!");
                }
            }
        }
    }
}