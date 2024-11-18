using EuroBuld.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        EuroBuldEntities7 _context;

        public AdminPage()
        {
            InitializeComponent();
            _context = new EuroBuldEntities7();
            LoadUsers();
            this.DataContext = this;
        }

        private void LoadData<T>(IQueryable<T> data)
        {
            usersDataGrid.Columns.Clear();
            usersDataGrid.ItemsSource = data.ToList();

            foreach (var property in typeof(T).GetProperties())
            {
                string header = property.Name.StartsWith("ID")
                    ? "Id_" + property.Name.Substring(2)
                    : property.Name;

                usersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = header,
                    Binding = new System.Windows.Data.Binding(property.Name),
                    Width = new DataGridLength(200)
                });
            }
        }


        private void LoadUsers()
        {
            LoadData(_context.Users);
            usersDataGrid.Tag = "Users";
        }


        private void LoadServices()
        {
            LoadData(_context.Service);
            usersDataGrid.Tag = "Services";
        }


        private void LoadRequests()
        {
            LoadData(_context.Requests);
            usersDataGrid.Tag = "Requests";
        }


        private void LoadStaff()
        {
            LoadData(_context.Staff);
            usersDataGrid.Tag = "Staff";
        }


        private void LoadOrders()
        {
            LoadData(_context.Processed_customer_orders);
            usersDataGrid.Tag = "Orders";
        }


        private void LoadRole()
        {
            LoadData(_context.Role);
            usersDataGrid.Tag = "Role";
        }

       
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            string currentTable = usersDataGrid.Tag.ToString();

            if (currentTable == "Users")
            {
                UserAdd userAddForm = new UserAdd();
                userAddForm.Show();
            }
            else if (currentTable == "Services")
            {
                ServiceAdd serviceAddForm = new ServiceAdd();
                serviceAddForm.Show();
            }
            //else if (currentTable == "Requests")
            //{
            //    RequestAdd requestAddForm = new RequestAdd();
            //    requestAddForm.Show();
            //}
            //else if (currentTable == "Staff")
            //{
            //    StaffAdd staffAddForm = new StaffAdd();
            //    staffAddForm.Show();
            //}
            //else if (currentTable == "Orders")
            //{
            //    OrderAdd orderAddForm = new OrderAdd();
            //    orderAddForm.Show();
            //}
            else if (currentTable == "Role")
            {
                RollAdd roleAddForm = new RollAdd();
                roleAddForm.Show();
            }
            else
            {
                MessageBox.Show("Невозможно открыть форму для текущей таблицы.");
            }
        }

        
        private void LoadClients_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }


        private void LoadRequests(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }


        private void LoadRole(object sender, RoutedEventArgs e)
        {
            LoadRole();
        }


        private void LoadServices_Click(object sender, RoutedEventArgs e)
        {
            LoadServices();
        }


        private void LoadOrders_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }


        private void LoadStaff_Click(object sender, RoutedEventArgs e)
        {
            LoadStaff();
        }


        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = usersDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var entityType = selectedItem.GetType();
                var entitySet = _context.Set(entityType);

                int currentStaffId = Authorization.StaffId;

                var passwordWindow = new PasswordWindow(currentStaffId);
                if (passwordWindow.ShowDialog() == true)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            entitySet.Remove(selectedItem);
                            _context.SaveChanges();
                            LoadServices();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Неверный пароль администратора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }

}
