using EuroBuld.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
			LoadUserData();

			EmailTextBox.Text = Authorization.CurrentUser.Email;
			PasswordTextBox.Text = Authorization.CurrentUser.Password;
		}


		private void LoadUserData()
		{
			try
			{
				var currentUser = Authorization.CurrentUser;

				NameTextBox.Text = currentUser.First_name;
				SurnameTextBox.Text = currentUser.Last_name;
				PatronnymicTextBox.Text = currentUser.Patronymic;
				PhoneTextBox.Text = currentUser.Phone;
				PassportTextBox.Text = currentUser.Passport_details;
				EmailTextBox.Text = currentUser.Email;
				AddressTextBox.Text = currentUser.Addres;
				PasswordTextBox.Text = currentUser.Password;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
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
			try
			{
				if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
				{
					MessageBox.Show("Пароль не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				var currentUser = Authorization.CurrentUser;
				currentUser.First_name = NameTextBox.Text.Trim();
				currentUser.Last_name = SurnameTextBox.Text.Trim();
				currentUser.Patronymic = PatronnymicTextBox.Text.Trim();
				currentUser.Phone = PhoneTextBox.Text.Trim();
				currentUser.Passport_details = PassportTextBox.Text.Trim();
				currentUser.Email = EmailTextBox.Text.Trim();
				currentUser.Addres = AddressTextBox.Text.Trim();
				currentUser.Password = PasswordTextBox.Text.Trim();

				using (var db = new EuroBuldEntities16())
				{
					var userInDb = db.Users.Find(currentUser.ID_Users);
					if (userInDb != null)
					{
						userInDb.First_name = currentUser.First_name;
						userInDb.Last_name = currentUser.Last_name;
                        userInDb.Patronymic = currentUser.Patronymic;
                        userInDb.Number_Phone = currentUser.Phone;
                        userInDb.Passport_details = currentUser.Passport_details;
						userInDb.Address = currentUser.Addres;
                        userInDb.Email = currentUser.Email;
						userInDb.Password = currentUser.Password;

						db.SaveChanges();
						MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else
					{
						MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка обновления данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


		private bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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
