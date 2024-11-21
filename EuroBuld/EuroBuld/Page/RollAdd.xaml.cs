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
using Xceed.Wpf.Toolkit;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для RollAdd.xaml
    /// </summary>
    public partial class RollAdd : Window
    {
        private EuroBuldEntities1 _context;
        public RollAdd()
        {
            InitializeComponent();
            _context = new EuroBuldEntities1();
        }

        private void SendRoll_Click(object sender, RoutedEventArgs e)
        {
            string NameRoll = NameRollTextBox.Text;
            string salary = SalaryTextBox.Text;

            if (string.IsNullOrEmpty(NameRoll) || string.IsNullOrEmpty(salary))
            {
                System.Windows.MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            var newRole = new Role
            {
                roll_name = NameRoll,
                salary = salary
            };

            System.Windows.MessageBox.Show("Роль добавлена");

            _context.Role.Add(newRole);

            _context.SaveChanges();
            this.Close();
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
        }

    }
}
