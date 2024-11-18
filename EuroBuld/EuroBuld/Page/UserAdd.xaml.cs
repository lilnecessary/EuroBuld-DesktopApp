using EuroBuld.DataBase;
using System;
using System.Linq;
using System.Windows;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        private EuroBuldEntities7 _context;

        public UserAdd()
        {
            InitializeComponent();
            _context = new EuroBuldEntities7();
        }

        private void AddUser1_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            string numberPhone = numberPhoneTextBox.Text;
            string address = addressTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string patronymic = patronymicTextBox.Text;
            string passportDetails = passportDetailsTextBox.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(numberPhone) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic) || string.IsNullOrEmpty(passportDetails))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            var newUser = new Users
            {
                Email = email,
                Password = password,
                Number_Phone = numberPhone,
                //Address = address,
                First_name = firstName,
                Last_name = lastName,
                Patronymic = patronymic,
                Passport_details = passportDetails
            };
            MessageBox.Show("Пользователь добавлен");

            _context.Users.Add(newUser);
            _context.SaveChanges();

            this.Close();
            AdminPage adminPage = new AdminPage();
            adminPage.Show(); 
        }

    }
}
