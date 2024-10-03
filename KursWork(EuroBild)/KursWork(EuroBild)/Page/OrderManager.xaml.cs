using KursWork_EuroBild_.DataBase;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using static KursWork_EuroBild_.Page.ManagerPage;

namespace KursWork_EuroBild_.Page
{
    public partial class OrderManager : Window
    {
        private сustomer selectedCustomer;
        private int currentManagerId; // ID текущего менеджера
        public event Action OrderCompleted;
        private int idCustomers;
    private int selectedForemanId; // Инициализируйте это значение при выборе бригадира
    private string projectName;
    private string constructionStatus; // Заполните это значение из текстового поля или другого источника
    private DateTime startDate;
    private DateTime endDate;
    private string price; // Аналогично, заполните это значение
        public OrderManager(int managerId, сustomer customer)
        {
            InitializeComponent();
            currentManagerId = managerId; // Сохраняем ID менеджера
            selectedCustomer = customer;
            FillCustomerDetails();
            LoadOrdersForManager(); // Загружаем заказы для менеджера
        }

        // Метод для заполнения полей формы данными выбранного клиента
        private void FillCustomerDetails()
        {
            if (selectedCustomer != null)
            {
                ProjectNameTextBox.Text = "Проект " + selectedCustomer.UserName; // Пример заполнения
            }
        }


        // Логика сохранения заказа
        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new EuroBuldEntities())
            {
                try
                {
                    var newOrder = new customers_order
                    {
                        ID_customers = selectedCustomer.ID_customers,
                        Manager_ID = GetCurrentManagerID(),
                        Foreman_ID = int.Parse(ForemanIDTextBox.Text),
                        Project_Name = ProjectNameTextBox.Text,
                        Construction_Status = StatusTextBox.Text,
                        Date_Start = DateTime.Parse(DateStartTextBox.Text),
                        Date_Ending = DateTime.Parse(DateEndTextBox.Text),
                        Price = PriceTextBox.Text
                    };

                    context.customers_order.Add(newOrder);

                    // Удаляем все заказы, связанные с клиентом
                    var ordersToRemove = context.customers_order
                        .Where(o => o.ID_customers == selectedCustomer.ID_customers)
                        .ToList();

                    foreach (var order in ordersToRemove)
                    {
                        context.customers_order.Remove(order);
                    }

                    // Удаляем клиента
                    var customerToRemove = context.customers.FirstOrDefault(c => c.ID_customers == selectedCustomer.ID_customers);
                    if (customerToRemove != null)
                    {
                        context.customers.Remove(customerToRemove);
                    }

                    context.SaveChanges(); // Сохранение изменений в БД
                    MessageBox.Show("Заказ успешно сохранен.");

                    OrderCompleted?.Invoke();
                    this.Close();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
            }
        }
        private void SaveOrder()
        {
            using (var context = new EuroBuldEntities())
            {
                var order = new customers_order
                {
                    ID_customers = idCustomers, // Убедитесь, что здесь используется правильное значение
                    Manager_ID = currentManagerId,
                    Foreman_ID = selectedForemanId,
                    Project_Name = projectName,
                    Construction_Status = constructionStatus,
                    Date_Start = startDate,
                    Date_Ending = endDate,
                    Price = price
                };

                context.customers_order.Add(order);
                context.SaveChanges(); // Это сохранит изменения в базе данных
            }
        }

        private void LoadOrdersForManager()
        {
            using (var context = new EuroBuldEntities())
            {
                var orders = context.customers_order
                    .Where(o => o.Manager_ID == currentManagerId) // Замените на актуальный идентификатор менеджера
                    .Select(o => new
                    {
                        o.ID_Сustomers_order,
                        CustomerName = context.customers
                                          .Where(c => c.ID_customers == o.ID_customers)
                                          .Select(c => c.ID_Users) // Получите пользователя по ID_Users
                                          .FirstOrDefault(),
                        o.Project_Name,
                        o.Construction_Status,
                        o.Date_Start,
                        o.Date_Ending,
                        o.Price
                    }).ToList();

                OrdersDataGrid.ItemsSource = orders; // Привязка данных к DataGrid
            }
        }

        // В OrderManager.xaml.cs
        public OrderManager(сustomer selectedCustomer)
        {
            InitializeComponent();
            this.selectedCustomer = selectedCustomer;

            // Теперь используйте selectedCustomer.ID_customers для создания заказа
            idCustomers = selectedCustomer.ID_customers;
        }

        public class CustomersOrder
        {
            public int ID_Сustomers_order { get; set; }
            public int ID_customers { get; set; }
            public int Manager_ID { get; set; }
            public int Foreman_ID { get; set; }
            public string Project_Name { get; set; }
            public string Construction_Status { get; set; }
            public DateTime? Date_Start { get; set; }
            public DateTime? Date_Ending { get; set; }
            public string Price { get; set; }

            
        }



        // Пример метода для получения ID текущего менеджера
        private int GetCurrentManagerID()
        {
            return 2; // Пример: возвращаем фиксированный ID менеджера
        }
    }
}
