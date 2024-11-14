using EuroBuld.DataBase;
using System;
using System.Linq;
using System.Windows;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        private EuroBuldEntities7 _context;
        private int _staffId;

        public PasswordWindow(int staffId)
        {
            InitializeComponent();
            _context = new EuroBuldEntities7();
            _staffId = staffId;
        }

        public bool IsPasswordCorrect { get; private set; }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = _context.Staff.FirstOrDefault(s => s.ID_Staff == _staffId);

            if (currentUser != null && PasswordBox.Password == currentUser.Password)
            {
                IsPasswordCorrect = true;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
