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
        private EuroBuldEntities12 _context;
        private int _staffId;

        public ManagersOrders(int staffId)
        {
            InitializeComponent();
            _context = new EuroBuldEntities12();
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


		private void ManagerOrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (managerOrdersDataGrid.SelectedItem != null)
			{
				var selectedOrder = (Processed_customer_orders)managerOrdersDataGrid.SelectedItem;
				ShowOrderDetails(selectedOrder);
			}
		}

		private void ShowOrderDetails(Processed_customer_orders order)
		{
			var orderDetailsWindow = new OrderDetailsWindow(order.ID_Processed_customer_orders);
			orderDetailsWindow.Show();
		}



		private void SaveOrder_Click(object sender, RoutedEventArgs e)
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

	}
}
