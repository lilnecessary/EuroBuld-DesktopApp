using EuroBuld.DataBase;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace EuroBuld.Page
{
	public partial class OrderDetailsWindow : Window
	{
		private EuroBuldEntities13 _context;

		public OrderDetailsWindow(int orderId)
		{
			InitializeComponent();
			_context = new EuroBuldEntities13();
			LoadOrderDetails(orderId);
		}


        private void LoadOrderDetails(int orderId)
        {
            // Находим заказ в обработанных заказах
            var order = _context.Processed_customer_orders
                .FirstOrDefault(o => o.ID_Processed_customer_orders == orderId);

            if (order == null)
            {
                MessageBox.Show("Не найден заказ по заданному ID.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Находим исходный заказ в Customer_orders
            var customerOrder = _context.Customer_orders
                .FirstOrDefault(co => co.ID_Customers_orders == order.ID_Customer_orders);

            if (customerOrder == null)
            {
                MessageBox.Show("Не найден исходный заказ для этого обработанного заказа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Находим пользователя, который сделал заказ
            var user = _context.Users
                .FirstOrDefault(u => u.ID_Users == customerOrder.ID_Users);

            if (user == null)
            {
                MessageBox.Show($"Не найден пользователь, связанный с заказом. ID_Users: {customerOrder.ID_Users}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Выводим данные пользователя и заказа
            StringBuilder details = new StringBuilder();
            details.AppendLine($"Данные заказа:");
            details.AppendLine($"Статус: {order.Status_Orders.Name_Status ?? "Неизвестно"}");  // Использование Status_Orders для получения статуса
            details.AppendLine($"Дата начала: {order.Date_Start}");
            details.AppendLine($"Дата окончания: {order.Date_Ending}");
            details.AppendLine($"Окончательная сумма: {order.Final_sum}");

            details.AppendLine($"\nИнформация о заказчике:");
            details.AppendLine($"Имя: {user.First_name}");
            details.AppendLine($"Фамилия: {user.Last_name}");
            details.AppendLine($"Номер телефона: {user.Number_Phone}");
            details.AppendLine($"Адрес: {user.Address}");

            OrderDetailsTextBlock.Text = details.ToString();
        }


    }
}
