using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EuroBuld.DataBase;

namespace EuroBuld.Page
{
    public partial class ManagerPage : Window
    {
        private EuroBuldEntities16 _context;

        public ManagerPage()
        {
            InitializeComponent();
            _context = new EuroBuldEntities16();
            LoadCustomerOrders();
        }


        private void LoadCustomerOrders()
        {
            var orders = _context.Customer_orders
                                 .Where(o => o.Status != "Hide") 
                                 .ToList();
            UpdateDataGridColumns(orders);
            customerOrdersDataGrid.ItemsSource = orders;
        }


        private void LoadMyOrders()
        {
            var orders = _context.Processed_customer_orders
                .Where(o => o.ID_Staff == Authorization.StaffId)  
                .ToList();
            UpdateDataGridColumns(orders);
            customerOrdersDataGrid.ItemsSource = orders;
        }


        private void UpdateDataGridColumns(object data)
        {
            customerOrdersDataGrid.Columns.Clear();

            if (data is List<Customer_orders>)
            {
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Заказа",
                    Binding = new System.Windows.Data.Binding("ID_Customers_orders"),
                    Width = 100
                });
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Услуги",
                    Binding = new System.Windows.Data.Binding("ID_Service"),
                    Width = 100
                });
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Пользователя",
                    Binding = new System.Windows.Data.Binding("ID_Users"),
                    Width = 120
                });
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата заказа",
                    Binding = new System.Windows.Data.Binding("Order_Date"),
                    Width = 120
                });
            }
            else if (data is List<Processed_customer_orders>)
            {
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Обработанного заказа",
                    Binding = new System.Windows.Data.Binding("ID_Processed_order"),
                    Width = 100
                });
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Услуги",
                    Binding = new System.Windows.Data.Binding("ID_Service"),
                    Width = 100
                });
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Сотрудника",
                    Binding = new System.Windows.Data.Binding("ID_Staff"),
                    Width = 120
                });
                customerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата завершения",
                    Binding = new System.Windows.Data.Binding("Completion_Date"),
                    Width = 120
                });
            }
        }


        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerOrders();
        }


        private void MyOrders_Click(object sender, RoutedEventArgs e)
        {
            ManagersOrders managersOrdersWindow = new ManagersOrders(Authorization.StaffId);
            managersOrdersWindow.Show();
            this.Close();
        }


        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = customerOrdersDataGrid.SelectedItem as Customer_orders;
            if (selectedOrder != null)
            {
                TakeAnOrder takeOrderWindow = new TakeAnOrder(selectedOrder.ID_Customers_orders, Authorization.StaffId);
                takeOrderWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
