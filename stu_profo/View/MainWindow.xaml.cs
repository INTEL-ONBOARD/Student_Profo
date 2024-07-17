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
            System.Diagnostics.Debug.WriteLine(validateLoad());
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
    }
}