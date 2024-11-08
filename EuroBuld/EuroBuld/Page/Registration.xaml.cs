using EuroBuld.DataBase;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click_Authorization(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string repeatPassword = RepeatPasswordTextBox.Text.Trim();

            // Проверка, что пароли совпадают
            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают. Пожалуйста, повторите ввод.");
                return;
            }

            // Проверка, что email не пустой
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Введите правильный email.");
                return;
            }

            // Проверка, что пароль не пустой
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль.");
                return;
            }

            using (var context = new EuroBuldEntities5()) // Используйте ваш контекст базы данных
            {
                // Проверка, существует ли уже такой email
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким email уже существует.");
                    return;
                }

                var newUser = new Users
                {
                    Email = email,
                    Password = password,
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");

                this.Close();
                Authorization authorizationPage = new Authorization();
                authorizationPage.Show();
            }
        }


    }
}
