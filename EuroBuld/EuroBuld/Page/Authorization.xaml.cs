using EuroBuld.DataBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EuroBuld.Page
{
    public partial class Authorization : Window
    {
        public static string UserEmail { get; private set; }
        public static int StaffId { get; private set; }
        public static int? UserID { get; private set; }

        public Authorization()
        {
            InitializeComponent();
        }


        private void Button_Click_Registration(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Visibility = Visibility.Collapsed;
        }


        private void Button_Click_Authoriathion(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            using (var context = new EuroBuldEntities1())
            {

                var staffUser = context.Staff
                    .FirstOrDefault(s => s.Email == email && s.Password == password);

                if (staffUser != null)
                {
                    var role = context.Role.FirstOrDefault(r => r.ID_Role == staffUser.ID_Role);

                    if (role != null)
                    {
                        StaffId = staffUser.ID_Staff; 
                        if (role.roll_name == "Admin")
                        {
                            AdminPage adminPage = new AdminPage();
                            adminPage.Show();
                            this.Visibility = Visibility.Collapsed;
                        }
                        else if (role.roll_name == "Manager")
                        {
                            ManagerPage managerPage = new ManagerPage();
                            managerPage.Show();
                            this.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("У вас нет прав доступа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Роль не найдена для сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    var user = context.Users
                        .FirstOrDefault(u => u.Email == email && u.Password == password);

                    if (user != null)
                    {
                        UserEmail = user.Email;
                        Authorization.UserID = user.ID_Users;

                        PersonalAccount personalAccount = new PersonalAccount();
                        personalAccount.Show();
                        this.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

    }
}
