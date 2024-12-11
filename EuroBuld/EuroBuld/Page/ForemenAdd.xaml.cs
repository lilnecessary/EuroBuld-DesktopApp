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
    /// Логика взаимодействия для ForemenAdd.xaml
    /// </summary>
    public partial class ForemenAdd : Window
    {
        EuroBuldEntities16 _context;

        public ForemenAdd()
        {
            InitializeComponent();
            _context = new EuroBuldEntities16();
        }


        private async void SaveForeman_Click(object sender, RoutedEventArgs e)
        {
            var newForemen = new EuroBuld.DataBase.Foremen
            {
                First_Name = First_Name_TextBox.Text,
                Last_Name = LastName_TextBox.Text,
                Patronymic = PatronymicName_TextBox.Text,
                Number_of_Workers = int.TryParse(Number_phone_TextBox.Text, out int numberOfWorkers) ? numberOfWorkers : (int?)null,
                Number_phone = Number_phone_TextBox.Text,
            };

            try
            {
                _context.Foremen.Add(newForemen);
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
