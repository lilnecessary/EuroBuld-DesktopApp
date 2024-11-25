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
    /// Логика взаимодействия для ManagersOrders.xaml
    /// </summary>
    public partial class ManagersOrders : Window
    {
        private EuroBuldEntities10 _context;
        private int _staffId;

        public ManagersOrders(int staffId)
        {
            InitializeComponent();
            _context = new EuroBuldEntities10();
            _staffId = staffId;
            LoadManagerOrders();
        }


        private void LoadManagerOrders()
        {
            var orders = _context.Processed_customer_orders
                .Where(o => o.ID_Staff == _staffId)
                .ToList();
            UpdateDataGridColumns(orders);
            managerOrdersDataGrid.ItemsSource = orders;
        }


        private void UpdateDataGridColumns(object data)
        {
            managerOrdersDataGrid.Columns.Clear();

            if (data is List<Processed_customer_orders>)
            {
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Обработанного заказа",
                    Binding = new System.Windows.Data.Binding("ID_Processed_customer_orders"),
                    Width = DataGridLength.Auto 
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Заказа клиента",
                    Binding = new System.Windows.Data.Binding("ID_Customer_orders"),
                    Width = DataGridLength.Auto 
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Сотрудника",
                    Binding = new System.Windows.Data.Binding("ID_Staff"),
                    Width = DataGridLength.Auto 
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID Прораба",
                    Binding = new System.Windows.Data.Binding("ID_Foreman"),
                    Width = DataGridLength.Auto 
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Статус",
                    Binding = new System.Windows.Data.Binding("Status"),
                    Width = DataGridLength.Auto 
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата начала",
                    Binding = new System.Windows.Data.Binding("Date_Start"),
                    Width = DataGridLength.Auto
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата окончания",
                    Binding = new System.Windows.Data.Binding("Date_Ending"),
                    Width = DataGridLength.Auto 
                });
                managerOrdersDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Окончательная сумма",
                    Binding = new System.Windows.Data.Binding("Final_sum"),
                    Width = DataGridLength.Auto
                });
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage managerPage = new ManagerPage();
            managerPage.Show();
            this.Close();
        }
    }
}
