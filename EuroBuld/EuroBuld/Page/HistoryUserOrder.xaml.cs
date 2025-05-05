﻿using EuroBuld.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для HistoryUserOrder.xaml
    /// </summary>
    public partial class HistoryUserOrder : Window
    {
        public ObservableCollection<ProcessedOrder> Orders { get; set; }

        public HistoryUserOrder()
        {
            InitializeComponent();
            LoadOrders();
            DataContext = this;
        }


        private void LoadOrders()
        {
            using (var context = new EuroBuldEntities17())
            {
                int currentUserId = Authorization.CurrentUser.ID_Users;

                var allowedStatuses = new[] { "отменен", "выполнен" };

                var orders = context.Processed_customer_orders
                    .Where(order =>
                        order.Customer_orders.ID_Users == currentUserId &&
                        allowedStatuses.Contains(order.Status_Orders.Name_Status))
                    .Select(order => new ProcessedOrder
                    {
                        OrderID = order.ID_Processed_customer_orders,
                        OrderDate = order.Date_Start,
                        Status = order.Status_Orders.Name_Status ?? "Unknown",
                        DateEnding = order.Date_Ending,
                        FinalSum = order.Final_sum,
                        Items = context.Service
                            .Where(s => s.ID_Service == order.Customer_orders.ID_Service)
                            .Select(s => s.Item_Name).ToList()
                    })
                    .ToList();

                Orders = new ObservableCollection<ProcessedOrder>(orders);
            }

            OrdersList.ItemsSource = Orders;
        }


        public class ProcessedOrder
        {
            public int OrderID { get; set; }
            public DateTime? OrderDate { get; set; }
            public string Status { get; set; }
            public DateTime? DateEnding { get; set; }
            public string FinalSum { get; set; }
            public List<string> Items { get; set; }
        }


        private void Button_Click_MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Close();
        }


        private void Button_Click_AboutUs(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }


        private void Button_Click_PersonalAcount(object sender, RoutedEventArgs e)
        {
            PersonalAccount personalAccount = new PersonalAccount();
            personalAccount.Show();
            this.Close();
        }


        private void Button_Click_Orders(object sender, RoutedEventArgs e)
        {
            UserOrder order = new UserOrder();
            order.Show();
            this.Close();
        }


        private void TextBlock_MouseDown_MainWindow(object sender, MouseButtonEventArgs e)
        {
            Button_Click_PersonalAcount(sender, e);
        }


        private void TextBlock_MouseDown_UserOrder(object sender, MouseButtonEventArgs e)
        {
            Button_Click_Orders(sender, e);
        }
    }
}
