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

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

			if (Authorization.CurrentUser != null)
			{
				LoginButton.Visibility = Visibility.Collapsed;
				RegistrationButton.Visibility = Visibility.Collapsed;

				UserEmailTextBlock.Text = Authorization.CurrentUser.Email;
				UserEmailTextBlock.Visibility = Visibility.Visible;			
			}
			else
			{
				LoginButton.Visibility = Visibility.Visible;
				RegistrationButton.Visibility = Visibility.Visible;			
			}
		}


		private void Button_Click_Authorization(object sender, RoutedEventArgs e)
		{
			Authorization authorization = new Authorization();
			authorization.Show();
			this.Close();

			if (Authorization.CurrentUser != null)
			{
				LoginButton.Visibility = Visibility.Collapsed;
				RegistrationButton.Visibility = Visibility.Collapsed;

				UserEmailTextBlock.Text = Authorization.CurrentUser.Email;
				UserEmailTextBlock.Visibility = Visibility.Visible;			
			}
		}


		private void UserEmailTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			PersonalAccount personalAccount = new PersonalAccount();
			personalAccount.Show();
			this.Visibility = Visibility.Collapsed;
			this.Close();
		}


		private void Button_Click_PersonalAccount(object sender, RoutedEventArgs e)
		{
			PersonalAccount personalAccount = new PersonalAccount();
			personalAccount.Show();
			this.Visibility = Visibility.Collapsed;

		}


		private void Button_Click_Registration(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


        private void Button_Click_AboutUs(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


        private void Button_Click_Contact(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }
    }
}
