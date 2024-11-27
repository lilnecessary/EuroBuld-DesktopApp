using EuroBuld.DataBase;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EuroBuld.Page
{
	public partial class ServiceCard : Window
	{
		private ServiceViewModel SelectedService { get; set; }

		public ServiceCard(ServiceViewModel selectedService)
		{
			InitializeComponent();
            LoadCarsAsync();

            if (selectedService == null)
				throw new ArgumentNullException(nameof(selectedService));

			SelectedService = selectedService;

			ServiceNameText.Text = selectedService.Item_Name;
			ServicePriceText.Text = $"{selectedService.Price:C}";

			ServiceDescriptionText.Text = string.IsNullOrEmpty(selectedService.Item_Description)
				? "Описание не доступно"
				: selectedService.Item_Description;

			if (selectedService.Image != null && selectedService.Image.Length > 0)
			{
				ServiceImage.Source = ByteArrayToImage(selectedService.Image);
			}
			else
			{
				ServiceImage.Visibility = Visibility.Hidden;
			}
		}


        public async Task<List<ServiceViewModel>> GetAllCarsAsync()
        {
            using (var context = new EuroBuldEntities13())
            {
                var allServices = await context.Service
                    .Select(service => new ServiceViewModel
                    {
                        ServiceID = service.ID_Service,
                        Item_Name = service.Item_Name,
                        Item_Description = service.Item_Description,
                        Price = service.Price,
                        Image = service.Image
                    })
                    .ToListAsync();

                var randomServices = allServices
                    .OrderBy(s => Guid.NewGuid())
                    .Take(5)
                    .ToList();

                return randomServices;
            }
        }


        private ImageSource ByteArrayToImage(byte[] byteArray)
		{
			if (byteArray == null)
				return null;

			using (MemoryStream stream = new MemoryStream(byteArray))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = stream;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				return bitmapImage;
			}
		}


        private void Button_Click_Buy(object sender, RoutedEventArgs e)
        {
            if (SelectedService == null)
            {
                MessageBox.Show("Услуга не выбрана. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Authorization.CurrentUser == null)
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь перед созданием заказа.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var context = new EuroBuldEntities13())
            {
                var user = context.Users.FirstOrDefault(u => u.ID_Users == Authorization.CurrentUser.ID_Users);

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.First_name) || string.IsNullOrEmpty(user.Last_name) ||
                    string.IsNullOrEmpty(user.Number_Phone) || string.IsNullOrEmpty(user.Address) ||
                    string.IsNullOrEmpty(user.Patronymic) || string.IsNullOrEmpty(user.Passport_details))
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные данные в личном кабинете, для оформления заказа.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newOrder = new Customer_orders
                {
                    ID_Service = SelectedService.ServiceID,
                    ID_Users = Authorization.CurrentUser.ID_Users,
                    Order_Date = DateTime.Now
                };

                context.Customer_orders.Add(newOrder);
                context.SaveChanges();

                try
                {
                    string receipt = GenerateReceipt(newOrder, user);

                    var sendCheckRole = context.Role.FirstOrDefault(r => r.roll_name == "SendCheck");
                    if (sendCheckRole == null)
                    {
                        MessageBox.Show("Роль SendCheck не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var sendCheckStaff = context.Staff
                        .FirstOrDefault(s => s.ID_Role == sendCheckRole.ID_Role);

                    if (sendCheckStaff == null || string.IsNullOrEmpty(sendCheckStaff.Email))
                    {
                        MessageBox.Show("Не удалось найти сотрудника с ролью SendCheck или почта не указана.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string sendCheckEmail = sendCheckStaff.Email;
                    string sendCheckEmailPassword = sendCheckStaff.Password;

                    SendEmail(user.Email, "Ваш заказ в EuroBuld", receipt, sendCheckEmail, sendCheckEmailPassword);

                    MessageBox.Show("Заказ успешно сохранён. Чек отправлен на вашу почту.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отправке чека: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private async void LoadCarsAsync()
        {
            var services = await GetAllCarsAsync();
            CarsItemsControl.ItemsSource = services.Take(5); 
        }


        private void Button_Click_buy(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedService = button?.DataContext as ServiceViewModel;

            if (selectedService != null)
            {
                ServiceCard serviceCard = new ServiceCard(selectedService);
                serviceCard.Show();
                this.Visibility = Visibility.Collapsed;
            }
        }


        private void SendEmail(string toEmail, string subject, string body, string sendCheckEmail, string sendCheckEmailPassword)
		{
			try
			{
				using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
				{
					smtpClient.Credentials = new NetworkCredential(sendCheckEmail, "jlyx djso obwj vkvu");
					smtpClient.EnableSsl = true;

					var mailMessage = new MailMessage
					{
						From = new MailAddress(sendCheckEmail, "EuroBuld"),
						Subject = subject,
						Body = body,
						IsBodyHtml = false
					};

					mailMessage.To.Add(toEmail);

					smtpClient.Send(mailMessage);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при отправке письма: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


		private string GenerateReceipt(Customer_orders order, Users user)
		{
			using (var context = new EuroBuldEntities13())
			{
				var service = context.Service.FirstOrDefault(s => s.ID_Service == order.ID_Service);
				if (service == null)
				{
					return "Услуга не найдена.";
				}

				var receipt = new StringBuilder();
				receipt.AppendLine($"Чек для пользователя {user.First_name} {user.Last_name}");
				receipt.AppendLine($"Услуга: {service.Item_Name}");
				receipt.AppendLine($"Описание: {service.Item_Description}");
				receipt.AppendLine($"Цена к оплате: {service.Price}");
				receipt.AppendLine($"Дата заказа: {order.Order_Date?.ToShortDateString() ?? "Дата не указана"}");
				receipt.AppendLine("Для оплаты заказа напишите нам");
				receipt.AppendLine("Спасибо за заказ в EuroBuld!");

				return receipt.ToString();
			}
		}


		private void Button_Click_Service(object sender, RoutedEventArgs e)
		{
			Service service = new Service();
			service.Show();
			this.Visibility = Visibility.Collapsed;
			this.Close();
		}


		private void Button_Click_AboutUs(object sender, RoutedEventArgs e)
		{
			AboutUs aboutUs = new AboutUs();
			aboutUs.Show();
			this.Visibility = Visibility.Collapsed;
			this.Close();
		}


		private void Button_Click_Contact(object sender, RoutedEventArgs e)
		{
			Contact contact = new Contact();
			contact.Show();
			this.Visibility = Visibility.Collapsed;
			this.Close();
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			this.Visibility = Visibility.Collapsed;
			this.Close();
		}
	}
}
