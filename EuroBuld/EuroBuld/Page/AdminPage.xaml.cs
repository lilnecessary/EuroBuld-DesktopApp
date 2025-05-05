using DocumentFormat.OpenXml.Office2016.Excel;
using EuroBuld.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
        EuroBuldEntities17 _context;

        public AdminPage()
        {
            InitializeComponent();
            _context = new EuroBuldEntities17();
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


        private void LoadForemen()
        {
            LoadData(_context.Foremen);
            usersDataGrid.Tag = "Foremen";
        }



        private void LoadStatus()
        {
            LoadData(_context.Status_Orders);
            usersDataGrid.Tag = "Status_Orders";
        }


        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            string currentTable = usersDataGrid.Tag.ToString();

            if (currentTable == "Users")
            {
                UserAdd userAddForm = new UserAdd();
                userAddForm.Show();
                this.Close();
            }
            else if (currentTable == "Services")
            {
                ServiceAdd serviceAddForm = new ServiceAdd();
                serviceAddForm.Show();
                this.Close();
            }
            else if (currentTable == "Foremen")
            {
                ForemenAdd foremenAdd = new ForemenAdd();
                foremenAdd.Show();
                this.Close();
            }
            else if (currentTable == "Staff")
            {
                StaffAdd staffAddForm = new StaffAdd();
                staffAddForm.Show();
                this.Close();
            }
            else if (currentTable == "Status_Orders")
            {
                Status_Orders status_Orders = new Status_Orders();
                status_Orders.Show();
                this.Close();
            }
            else if (currentTable == "Role")
            {
                RollAdd roleAddForm = new RollAdd();
                roleAddForm.Show();
                this.Close();
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


        private void LoadForemen(object sender, RoutedEventArgs e)
        {
            LoadForemen();
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


        private void leave_Click(object sender, RoutedEventArgs e)
        {
            var leave = MessageBox.Show($"Вы уверены, что хотите выйти?\nВаша работа может не сохраниться", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (leave == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Visibility = Visibility.Collapsed;
                this.Close();
            }
        }


        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
			{
				_context.SaveChanges();

				MessageBox.Show("Изменения успешно сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
        }


        private void LoadStatus(object sender, RoutedEventArgs e)
        {
            LoadStatus();
        }


        private void Load_Reports_Click(object sender, RoutedEventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }
    }
}
