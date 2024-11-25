using EuroBuld.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.Close();
        }


        private void Button_Click_Registration(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


        private void Button_Click_MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


		private async void LoadCarsAsync()
		{
			var services = await GetAllCarsAsync();
			CarsItemsControl.ItemsSource = services; 
		}


        public async Task<List<ServiceViewModel>> GetAllCarsAsync()
		{
			using (var context = new EuroBuldEntities10())
			{
				return await context.Service.Select(service => new ServiceViewModel
				{
					ServiceID = service.ID_Service,
					Item_Name = service.Item_Name,
                    Item_Description = service.Item_Description,
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
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();    
            contact.Show();
            this.Visibility= Visibility.Collapsed;
            this.Close();
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
    }
}