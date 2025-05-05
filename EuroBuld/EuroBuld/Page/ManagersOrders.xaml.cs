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
        private EuroBuldEntities17 _context;
        private int _staffId;


		public ManagersOrders(int staffId)
        {
            InitializeComponent();
            _context = new EuroBuldEntities17();
            _staffId = staffId;
            LoadManagerOrders();
        }


        private void LoadManagerOrders()
        {
            var orders = _context.Processed_customer_orders
                .Where(o => o.ID_Staff == _staffId)
                .ToList();
            managerOrdersDataGrid.ItemsSource = orders;
        }


		private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage managerPage = new ManagerPage();
            managerPage.Show();
            this.Close();
        }


        private void EditOrderWindow_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = managerOrdersDataGrid.SelectedItem;
            if (selectedOrder == null)
            {
                MessageBox.Show("Пожалуйста, выберите заказ для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedOrderId = (int)((dynamic)selectedOrder).ID_Processed_customer_orders;
            var order = _context.Processed_customer_orders.FirstOrDefault(o => o.ID_Processed_customer_orders == selectedOrderId);

            if (order == null)
            {
                MessageBox.Show("Не удалось найти выбранный заказ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var editWindow = new EditOrderWindow(order, _context);
            editWindow.ShowDialog();

            LoadManagerOrders();
        }


        public class Processed_customer_orders
        {
            public int ID_Processed_customer_orders { get; set; }
            public int? ID_User { get; set; }

            public virtual User User { get; set; }
        }


        public class User
        {
            public int ID_User { get; set; }
            public string Name { get; set; }
            public virtual ICollection<Processed_customer_orders> Orders { get; set; }
        }
    }
}
