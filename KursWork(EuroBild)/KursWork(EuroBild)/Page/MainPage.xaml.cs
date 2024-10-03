using KursWork_EuroBild_.DataBase;
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

namespace KursWork_EuroBild_.Page
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void VhodButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            using (var context = new EuroBuldEntities())
            {
                var user = context.Users.FirstOrDefault(u => u.email == login && u.password == password);
                if (user != null)
                {
                    switch (user.ID_Role)
                    {
                        case 1: // Например, это роль администратора
                                // Если у вас есть отдельная страница для администратора, вы можете добавить её здесь.
                            MessageBox.Show("Добро пожаловать, администратор!");
                            break;
                        case 2: // Роль менеджера
                                // Передайте ID пользователя в ManagerPage
                            ManagerPage managerPage = new ManagerPage(user.ID_Users); // Передайте ID пользователя
                            managerPage.Show();
                            this.Visibility = Visibility.Collapsed;
                            break;
                        default:
                            MessageBox.Show("Неправильная роль пользователя.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль.");
                }
            }
        }



        private void RegButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
