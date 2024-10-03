using KursWork_EuroBild_.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace KursWork_EuroBild_.Page
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Window
    {
        private сustomer selectedCustomer;
        private int currentManagerId; // ID текущего менеджера

        // Конструктор с параметром managerId
        public ManagerPage(int managerId)
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var customers = GetCustomers();
            if (customers != null && customers.Count > 0)
            {
                CustomersDataGrid.ItemsSource = customers;
            }
            else
            {
                MessageBox.Show("Нет данных для отображения.");
            }
        }

        private void CustomersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Проверяем, что выбран элемент
            if (CustomersDataGrid.SelectedItem != null)
            {
                // Сохраняем выбранного клиента в переменную selectedCustomer
                selectedCustomer = (сustomer)CustomersDataGrid.SelectedItem;
            }
        }

        public List<сustomer> GetCustomers()
        {
            using (var context = new EuroBuldEntities())
            {
                return context.customers
                    .Select(c => new сustomer
                    {
                        ID_customers = c.ID_customers,
                        ID_Users = c.ID_Users ?? 0,
                        ID_service = c.ID_service ?? 0,
                        Order_Date = c.Order_Date,
                        UserName = context.Users
                                    .Where(u => u.ID_Users == c.ID_Users)
                                    .Select(u => u.email)
                                    .FirstOrDefault(),
                        Item_Name = context.service
                                    .Where(s => s.ID_service == c.ID_service)
                                    .Select(s => s.Item_Name)
                                    .FirstOrDefault()
                    }).ToList();
            }
        }

        public class сustomer
        {
            public int ID_customers { get; set; }
            public int ID_Users { get; set; }
            public int ID_service { get; set; }
            public DateTime? Order_Date { get; set; }

            public string UserName { get; set; }
            public string Item_Name { get; set; }
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                // Передайте целиком объект selectedCustomer в OrderManager
                OrderManager orderForm = new OrderManager(selectedCustomer);
                orderForm.ShowDialog(); // Ожидаем, пока пользователь заполнит данные
                LoadCustomers(); // Обновляем список клиентов после добавления заказа
            }
            else
            {
                MessageBox.Show("Выберите клиента для добавления заказа.");
            }
        }
    }
}
