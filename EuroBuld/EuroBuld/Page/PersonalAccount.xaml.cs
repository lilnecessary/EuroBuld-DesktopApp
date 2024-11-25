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
        


        private void FormatPhoneNumber()
        {
            string phoneNumber = PhoneTextBox.Text.Trim();

            phoneNumber = phoneNumber.Replace(" ", "").Replace("-", "");

            if (!phoneNumber.StartsWith("+"))
            {
               
                var countryCodes = new Dictionary<string, string>
                {
                    { "7", "+7" },   // Россия, Казахстан
                    { "33", "+373" }, // Молдова
                    { "44", "+374" }, // Армения
                    { "50", "+375" }, // Беларусь
                    { "51", "+992" }, // Таджикистан
                    { "52", "+996" }, // Кыргызстан
                    { "53", "+998" }, // Узбекистан
                    { "54", "+996" }, // Кыргызстан (вариант для короткого префикса)
                    { "55", "+380" }, // Украина
                    { "56", "+999" }  // Туркмения (опционально)
                };

                bool validCountryCode = false;

                foreach (var code in countryCodes.Keys)
                {
                    if (phoneNumber.StartsWith(code))
                    {
                        validCountryCode = true;
                        phoneNumber = countryCodes[code] + " " + phoneNumber.Substring(code.Length);
                        break;
                    }
                }

                if (!validCountryCode)
                {
                    MessageBox.Show("Неизвестный формат номера.");
                    return;
                }
            }

            PhoneTextBox.Text = Regex.Replace(phoneNumber, @"(\+\d{1,3})(\d{1,3})(\d{1,3})(\d{1,4})(\d+)", "$1 $2 $3 $4 $5");
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FormatPhoneNumber();
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


	}
}
