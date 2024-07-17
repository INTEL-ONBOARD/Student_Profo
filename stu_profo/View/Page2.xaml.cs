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
using System.Windows.Shapes;

using stu_profo.Model;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Window
    {
        public Page2()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Trigger binding update
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Trigger binding update
        }

<<<<<<< HEAD
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is your message.");
            Console.WriteLine("Fuck You ");
=======
        private void clickme(object sender, RoutedEventArgs e)
        {

>>>>>>> cd1a8a93efb4d653d6b23393757bc5b15170bab4
        }
    }
}
