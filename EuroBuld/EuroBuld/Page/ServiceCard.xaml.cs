using EuroBuld.DataBase;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EuroBuld.Page
{
    public partial class ServiceCard : Window
    {
        private ServiceViewModel SelectedService { get; set; }

        public ServiceCard(ServiceViewModel selectedService)
        {
            InitializeComponent();

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

            if (Authorization.UserID == null)
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь перед созданием заказа.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var context = new EuroBuldEntities1())
            {
                var newOrder = new Customer_orders
                {
                    ID_Service = SelectedService.ServiceID,
                    ID_Users = Authorization.UserID.Value,
                    Order_Date = DateTime.Now
                };

                context.Customer_orders.Add(newOrder);
                context.SaveChanges();

                MessageBox.Show("Заказ успешно сохранён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
