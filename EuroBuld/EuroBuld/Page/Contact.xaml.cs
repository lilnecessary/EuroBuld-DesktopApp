using EuroBuld.DataBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EuroBuld.Page
{
    public partial class Contact : Window
    {
        public Contact()
        {
            InitializeComponent();
            LoadGoogleMap();
            LoadServices();
        }


        private void LoadServices()
        {
            using (var context = new EuroBuldEntities7())
            {
                var services = context.Service.ToList();
                ServiceComboBox.ItemsSource = services;
            }
        }


        private async void LoadGoogleMap()
        {
            string googleMapUrl = "https://maps.app.goo.gl/aycxSABty2mXM5mR7";
            await CartaBrowser.EnsureCoreWebView2Async();
            CartaBrowser.Source = new Uri(googleMapUrl);
        }


        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }


        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {
            Service seervice = new Service();
            seervice.Show();
            this.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Visibility = Visibility.Visible;
        }


        private void Button_Click_Contact(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Visibility = Visibility.Visible;
        }


        private void SubmitRequestButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new EuroBuldEntities7())
            {
                var request = new Requests
                {
                    ID_Service = (int)ServiceComboBox.SelectedValue,
                    Request_Date = DateTime.Now,
                    First_name = FirstNameTextBox.Text,
                    Last_name = LastNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    Additional_Info = AdditionalInfoTextBox.Text,
                    Status = "Pending"
                };

                context.Requests.Add(request);
                context.SaveChanges();
            }

            MessageBox.Show("Заявка успешно отправлена!");
        }



    }
}
