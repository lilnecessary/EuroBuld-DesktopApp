using EuroBuld.DataBase;
using System;
using System.Linq;
using System.Windows;
using System.Data.Entity.Validation;

namespace EuroBuld.Page
{
    public partial class TakeAnOrder : Window
    {
        private EuroBuldEntities12 _context;
        private int _customerOrderId;
        private int _staffId;

        public TakeAnOrder(int customerOrderId, int staffId)
        {
            InitializeComponent();
            _staffId = staffId;
            _customerOrderId = customerOrderId;
            _context = new EuroBuldEntities12();
            LoadForemenData();
            
        }


        private void LoadForemenData()
        {
            var foremen = _context.Foremen
                                  .Select(f => new
                                  {
                                      f.ID_Foreman,
                                      FirstName = f.First_Name,
                                      LastName = f.Last_Name,
                                      Patronymic = f.Patronymic
                                  })
                                  .ToList();

            var formattedForemen = foremen.Select(f => new
            {
                f.ID_Foreman,
                FullName = $"{f.FirstName} {f.LastName} {f.Patronymic}"
            }).ToList();

            ForemenComboBox.ItemsSource = formattedForemen;
            ForemenComboBox.DisplayMemberPath = "FullName";
            ForemenComboBox.SelectedValuePath = "ID_Foreman";
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var selectedForemanId = (int)ForemenComboBox.SelectedValue;
            var statusText = StatusTextBox.Text;

            if (string.IsNullOrEmpty(statusText))
            {
                MessageBox.Show("Пожалуйста, введите статус заказа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dateStart = BirthDatePicker.SelectedDate;
            var dateEnd = EmploymentDatePicker.SelectedDate;

            string finalSumText = FinalSumTextBox.Text;
            decimal finalSumDecimal;
            if (!decimal.TryParse(finalSumText, out finalSumDecimal))
            {
                MessageBox.Show("Введите корректную сумму.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var finalSum = finalSumDecimal.ToString("0.##") + " ₽";

            if (selectedForemanId == 0 || !dateStart.HasValue || !dateEnd.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Добавляем проверку на дату окончания
            if (dateEnd <= dateStart)
            {
                MessageBox.Show("Дата окончания не может быть раньше или совпадать с датой начала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new EuroBuldEntities12())
                {
                    Processed_customer_orders processedOrder = new Processed_customer_orders
                    {
                        ID_Customer_orders = _customerOrderId,
                        ID_Staff = _staffId,
                        ID_Foreman = selectedForemanId,
                        Status = statusText,
                        Date_Start = dateStart.Value,
                        Date_Ending = dateEnd.Value,
                        Final_sum = finalSum
                    };

                    context.Processed_customer_orders.Add(processedOrder);

                    var customerOrder = context.Customer_orders
                        .FirstOrDefault(o => o.ID_Customers_orders == _customerOrderId);
                    if (customerOrder != null)
                    {
                        customerOrder.Status = "Hide";
                    }

                    context.SaveChanges();
                    MessageBox.Show("Заказ успешно взят.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                string validationErrors = "";
                foreach (var validationError in dbEx.EntityValidationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        validationErrors += $"Property: {error.PropertyName}, Error: {error.ErrorMessage}\n";
                    }
                }
                MessageBox.Show($"Ошибка валидации: \n{validationErrors}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
