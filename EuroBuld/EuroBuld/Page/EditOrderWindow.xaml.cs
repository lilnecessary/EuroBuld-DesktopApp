using EuroBuld.DataBase;
using System;
using System.Linq;
using System.Windows;

namespace EuroBuld.Page
{
    public partial class EditOrderWindow : Window
    {
        private Processed_customer_orders _order;
        private EuroBuldEntities17 _context;

        public EditOrderWindow(Processed_customer_orders order, EuroBuldEntities17 context)
        {
            InitializeComponent();
            _order = order;
            _context = context;

            StatusComboBox.ItemsSource = _context.Status_Orders.ToList();

            StatusComboBox.SelectedValue = _order.ID_Status_Orders;

            StartDatePicker.SelectedDate = _order.Date_Start;
            EndDatePicker.SelectedDate = _order.Date_Ending;
            FinalSumTextBox.Text = _order.Final_sum;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StatusComboBox.SelectedValue != null &&
                    (int)StatusComboBox.SelectedValue != _order.ID_Status_Orders)
                {
                    _order.ID_Status_Orders = (int)StatusComboBox.SelectedValue;
                }

                if (StartDatePicker.SelectedDate.HasValue)
                {
                    _order.Date_Start = StartDatePicker.SelectedDate.Value;
                }

                if (EndDatePicker.SelectedDate.HasValue)
                {
                    _order.Date_Ending = EndDatePicker.SelectedDate.Value;
                }

                if (!string.IsNullOrWhiteSpace(FinalSumTextBox.Text) &&
                    decimal.TryParse(FinalSumTextBox.Text, out decimal finalSum))
                {
                    _order.Final_sum = finalSum.ToString();
                }

                _context.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
