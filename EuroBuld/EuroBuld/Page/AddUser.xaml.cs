using EuroBuld.DataBase;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace EuroBuld.Page
{
    public partial class AddUser : Window
    {
        private EuroBuldEntities5 _context;
        private byte[] _Image;

        public AddUser()
        {
            InitializeComponent();
            _context = new EuroBuldEntities5();
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _Image = File.ReadAllBytes(openFileDialog.FileName);
                ImageFileNameTextBlock.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
            }
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(lastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(RoleTextBox.Text) ||
                _Image == null ||
                !DataBirthTextBox.SelectedDate.HasValue ||
                !DataEmployment.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля и выберите изображение.");
                return;
            }

            // Получение ID_Role из таблицы Role
            var role = _context.Role.FirstOrDefault(r => r.roll_name == RoleTextBox.Text);
            if (role == null)
            {
                MessageBox.Show("Должность не найдена. Проверьте правильность ввода.");
                return;
            }

            // Создание нового объекта Staff
            var newStaff = new Staff
            {
                ID_Role = role.ID_Role,
                Email = EmailTextBox.Text,
                Password = PasswordTextBox.Text,
                First_name = NameTextBox.Text,
                Last_name = lastNameTextBox.Text,
                Patronymic = PatronymicTextBox.Text,
                Passport_details = PassportdetailsTextBox.Text,
                Date_birth = DataBirthTextBox.SelectedDate.Value,
                Date_employment = DataEmployment.SelectedDate.Value,
                Image = _Image
            };

            try
            {
                // Добавление нового сотрудника в базу данных
                _context.Staff.Add(newStaff);
                await _context.SaveChangesAsync();
                MessageBox.Show("Сотрудник успешно добавлен!");

                // Очистка полей после успешного добавления
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении сотрудника: {ex.Message}");
            }
        }


        private void ClearFields()
        {
            EmailTextBox.Clear();
            PasswordTextBox.Clear();
            NameTextBox.Clear();
            lastNameTextBox.Clear();
            PatronymicTextBox.Clear();
            PassportdetailsTextBox.Clear();
            DataBirthTextBox.SelectedDate = null;
            DataEmployment.SelectedDate = null;
            _Image = null;
            ImageFileNameTextBlock.Text = string.Empty;
        }
    }
}
