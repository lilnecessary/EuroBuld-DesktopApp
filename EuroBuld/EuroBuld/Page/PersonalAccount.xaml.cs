using EuroBuld.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Window
    {
        public PersonalAccount()
        {
            InitializeComponent();

			EmailTextBox.Text = Authorization.CurrentUser.Email;
			PasswordTextBox.Text = Authorization.CurrentUser.Password;
		}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Visibility = Visibility.Visible;
            this.Close();
        }


        private void Button_Click_MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Visibility=Visibility.Visible;
            this.Close();
        }


        private void Button_Click_AboutUs(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Visibility=Visibility.Visible;
            this.Close();
        }


        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            this.Visibility=Visibility.Visible;
            this.Close();
        }


        private void Button_Click_DataRefresh(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            {
                MessageBox.Show("Пароль не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

        }


        private void Button_Click_СleatTextBox(object sender, RoutedEventArgs e)
        {
            NameTextBox.Clear();
            SurnameTextBox.Clear();
            PatronnymicTextBox.Clear();
            PhoneTextBox.Clear();
            PassportTextBox.Clear();
            PasswordTextBox.Clear();
            PasswordTextBox.Clear();
        }


        private void Button_Click_UserOrder(object sender, RoutedEventArgs e)
        {
            UserOrder userOrder = new UserOrder();
            userOrder.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


        private void Button_Click_HistoryUserOrder(object sender, RoutedEventArgs e)
        {
            HistoryUserOrder historyuserOrder = new HistoryUserOrder();
            historyuserOrder.Show();
            this.Visibility = Visibility.Collapsed;
			this.Close();
		}


		private void TextBlock_MouseDown_MainWindow(object sender, MouseButtonEventArgs e)
		{
			Button_Click_MainWindow(sender, e);
		}

		private void TextBlock_MouseDown_UserOrder(object sender, MouseButtonEventArgs e)
		{
			Button_Click_UserOrder(sender, e); 
		}

		private void TextBlock_MouseDown_HistoryUserOrder(object sender, MouseButtonEventArgs e)
		{
			Button_Click_HistoryUserOrder(sender, e); 
		}
	}
}
