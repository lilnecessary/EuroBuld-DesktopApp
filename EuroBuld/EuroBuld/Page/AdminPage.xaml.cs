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
        private byte[] _Image;

        public AdminPage()
        {
            InitializeComponent();
            _context = new EuroBuldEntities7();
            LoadUsers();
            this.DataContext = this;
        }

        // Универсальный метод для загрузки данных
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

        private void LoadClients_Click(object sender, RoutedEventArgs e)
        {
            LoadData(_context.Users);
        }

        private void LoadRequests(object sender, RoutedEventArgs e)
        {
            LoadData(_context.Requests);
        }

        private void LoadRole(object sender, RoutedEventArgs e)
        {
            LoadData(_context.Role);
        }

        private void LoadServices_Click(object sender, RoutedEventArgs e)
        {
            LoadData(_context.Service);
        }

        private void LoadOrders_Click(object sender, RoutedEventArgs e)
        {
            LoadData(_context.Processed_customer_orders);
        }

        private void LoadStaff_Click(object sender, RoutedEventArgs e)
        {
            LoadData(_context.Staff);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            //Добавление нового пользователя
            //AddUser addUser = new AddUser();
            //addUser.Show();
            //this.Visibility = Visibility.Visible;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            add Add = new add();
            Add.Show();
            this.Visibility = Visibility.Visible;
        }

        public class User
        {
            public int ID_Users { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Number_Phone { get; set; }
            public string Address { get; set; }
            public string First_name { get; set; }
            public string Last_name { get; set; }
            public string Patronymic { get; set; }
            public string Passport_details { get; set; }
        }


        private void LoadDataForCurrentTable()
        {
            if (usersDataGrid.Tag == "Users")
            {
                LoadUsers();
            }
            else if (usersDataGrid.Tag == "Services")
            {
                LoadServices();
            }
            else if (usersDataGrid.Tag == "Requests")
            {
                LoadRequests();
            }
            else if (usersDataGrid.Tag == "Staff")
            {
                LoadStaff();
            }
            else if (usersDataGrid.Tag == "Orders")
            {
                LoadOrders();
            }
            else if (usersDataGrid.Tag == "Role")
            {
                LoadRole();
            }
        }


        private void LoadUsers()
        {
            var users = _context.Users.ToList();
            usersDataGrid.ItemsSource = users;
            usersDataGrid.Tag = "Users";  
        }

        private void LoadServices()
        {
            var services = _context.Service.ToList();
            usersDataGrid.ItemsSource = services;
            usersDataGrid.Tag = "Services";  
        }

        private void LoadRequests()
        {
            var requests = _context.Requests.ToList();
            usersDataGrid.ItemsSource = requests;
            usersDataGrid.Tag = "Requests"; 
        }

        private void LoadStaff()
        {
            var staff = _context.Staff.ToList();
            usersDataGrid.ItemsSource = staff;
            usersDataGrid.Tag = "Staff"; 
        }

        private void LoadOrders()
        {
            var orders = _context.Processed_customer_orders.ToList();
            usersDataGrid.ItemsSource = orders;
            usersDataGrid.Tag = "Orders";  
        }

        private void LoadRole()
        {
            var roles = _context.Role.ToList();
            usersDataGrid.ItemsSource = roles;
            usersDataGrid.Tag = "Role"; 
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            UserAdd ADD = new UserAdd();
            ADD.Show();
            this.Visibility = Visibility.Collapsed;
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
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить запись?",
                                                 "Подтверждение удаления",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            entitySet.Remove(selectedItem);
                            _context.SaveChanges();
                            LoadDataForCurrentTable();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}",
                                             "Ошибка",
                                             MessageBoxButton.OK,
                                             MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Неверный пароль администратора.",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }



    }
}
