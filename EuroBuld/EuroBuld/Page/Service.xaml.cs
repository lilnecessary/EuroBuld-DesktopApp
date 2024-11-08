using EuroBuld.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для Service.xaml
    /// </summary>
    public partial class Service : Window
    {
        public Service()
        {
            InitializeComponent();
			LoadCarsAsync();
        }

        private void Button_Click_Authorization(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_Registration(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Visibility = Visibility.Collapsed;
        }

		private async void LoadCarsAsync()
		{
			var services = await GetAllCarsAsync();
			CarsItemsControl.ItemsSource = services; 
		}


		private void CarsItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//if (e.OriginalSource is FrameworkElement fe && fe.DataContext is CarViewModel selectedCar)
			//{
			//	// Открываем новое окно с подробной информацией об автомобиле
			//	var carDetailsWindow = new CarDetailsWindow(selectedCar);
			//	carDetailsWindow.Show();
			//}
		}

		public async Task<List<ServiceViewModel>> GetAllCarsAsync()
		{
			using (var context = new EuroBuldEntities5())
			{
				return await context.Service.Select(service => new ServiceViewModel
				{
					ServiceID = service.ID_Service,
					Item_Name = service.Item_Name,
					Price = service.Price,
					Image = service.Image
				}).ToListAsync();
			}
		}

        private void Button_Click_AboutUs(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Visibility= Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();    
            contact.Show();
            this.Visibility= Visibility.Collapsed;
        }
    }
}