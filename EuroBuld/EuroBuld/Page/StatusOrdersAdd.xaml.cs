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
    /// Логика взаимодействия для Status_Orders.xaml
    /// </summary>
    public partial class Status_Orders : Window
    {
        EuroBuldEntities15 _context;

        public Status_Orders()
        {
            InitializeComponent();
            _context = new EuroBuldEntities15();
        }

        private async void Save_Status_Click(object sender, RoutedEventArgs e)
        {
            var newStatus = new EuroBuld.DataBase.Status_Orders
            {
                Name_Status = Name_Status_TextBox.Text,
            };

            try
            {
                _context.Status_Orders.Add(newStatus);
                await _context.SaveChangesAsync();

                System.Windows.MessageBox.Show("Успешное добавление!");

                this.Close();
                AdminPage adminPage = new AdminPage();
                adminPage.Show();
            }
            catch (Exception ex)
            {
                var validationErrors = _context.GetValidationErrors();
                StringBuilder sb = new StringBuilder();
                foreach (var validationError in validationErrors)
                {
                    sb.AppendLine($"Entity: {validationError.Entry.Entity.GetType().Name}");
                    foreach (var error in validationError.ValidationErrors)
                    {
                        sb.AppendLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }

                System.Windows.MessageBox.Show(sb.ToString());
            }
        }
    }
}
