using EuroBuld.DataBase;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace EuroBuld.Page
{
    public partial class StaffAdd : Window
    {
        private EuroBuldEntities14 _context;
        private byte[] _image;

        public StaffAdd()
        {
            InitializeComponent();
            _context = new EuroBuldEntities14();
            LoadRoles();
        }


        private void LoadRoles()
        {
            var roles = _context.Role.ToList();
            RoleComboBox.ItemsSource = roles;
            RoleComboBox.DisplayMemberPath = "roll_name"; 
            RoleComboBox.SelectedValuePath = "ID_Role"; 
        }


        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _image = File.ReadAllBytes(openFileDialog.FileName);
                MessageBox.Show("Изображение добавлено успешно!");
            }
        }


        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (RoleComboBox.SelectedValue == null ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PatronymicTextBox.Text) ||
                string.IsNullOrWhiteSpace(PassportTextBox.Text) ||
                BirthDatePicker.SelectedDate == null ||
                EmploymentDatePicker.SelectedDate == null ||
                _image == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите изображение.");
                return;
            }

            var existingStaff = _context.Staff.FirstOrDefault(s => s.Email == EmailTextBox.Text.Trim() || s.Passport_details == PassportTextBox.Text.Trim());

            if (existingStaff != null)
            {
                MessageBox.Show("Сотрудник с таким Email или паспортными данными уже существует.");
                return;
            }

            var newStaff = new Staff
            {
                ID_Role = (int)RoleComboBox.SelectedValue,
                Email = EmailTextBox.Text.Trim(),
                Password = PasswordTextBox.Text.Trim(),
                First_name = FirstNameTextBox.Text.Trim(),
                Last_name = LastNameTextBox.Text.Trim(),
                Patronymic = PatronymicTextBox.Text.Trim(),
                Passport_details = PassportTextBox.Text.Trim(),
                Date_birth = BirthDatePicker.SelectedDate.Value,
                Date_employment = EmploymentDatePicker.SelectedDate.Value,
                Image = _image
            };

            try
            {
                _context.Staff.Add(newStaff);
                await _context.SaveChangesAsync();
                MessageBox.Show("Сотрудник успешно добавлен!");
                AdminPage adminPage = new AdminPage();
                adminPage.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }
    }
}
