using EuroBuld.DataBase;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для ServiceCard.xaml
    /// </summary>
    public partial class ServiceCard : Window
    {
        public ServiceCard(ServiceViewModel service)
        {
            InitializeComponent();

            // Заполняем текстовые элементы
            ServiceNameText.Text = service.Item_Name;
            ServicePriceText.Text = service.Price;

            // Добавлена проверка на пустоту для описания
            ServiceDescriptionText.Text = string.IsNullOrEmpty(service.Item_Description)
                ? "Описание не доступно"
                : service.Item_Description;

            // Обработка изображения
            if (service.Image != null && service.Image.Length > 0)
            {
                ServiceImage.Source = ByteArrayToImage(service.Image);
            }
            else
            {
                ServiceImage.Visibility = Visibility.Hidden;
            }
        }

        // Метод преобразования массива байтов в ImageSource для отображения
        private ImageSource ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null)
                return null;

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Включаем кэширование
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void Button_Click_Contact(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_AboutUs(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Buy(object sender, RoutedEventArgs e)
        {

        }
    }
}
