using EuroBuld.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Window
    {
        public AboutUs()
        {
            InitializeComponent();
            LoadStaff();
        }

        public class StaffInfo
        {
            public string First_name { get; set; }
            public string Last_name { get; set; }
            public byte[] Image { get; set; }
            public int? ID_Role { get; set; }
        }


        private void LoadStaff()
        {
            using (var context = new EuroBuldEntities16())
            {
                var staffList = context.Staff
                    .Join(context.Role,
                          staff => staff.ID_Role,
                          role => role.ID_Role,
                          (staff, role) => new { staff, role })
                    .Where(sr => sr.role.roll_name != "SendCheck")
                    .Select(sr => new StaffInfo
                    {
                        First_name = sr.staff.First_name,
                        Last_name = sr.staff.Last_name,
                        Image = sr.staff.Image,
                        ID_Role = sr.staff.ID_Role
                    })
                    .OrderBy(r => Guid.NewGuid())
                    .Take(4)
                    .ToList();

                DisplayStaff(staffList, context);
            }
        }


        private void DisplayStaff(List<StaffInfo> staffList, EuroBuldEntities16 context)
        {
            foreach (var staffMember in staffList)
            {
                var staffPanel = new StackPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 200,
                    Margin = new Thickness(10)
                };

                var border = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 240, 241, 245)),
                    Width = 150,
                    Height = 150,
                    CornerRadius = new CornerRadius(10)
                };

                var image = new Image
                {
                    Width = 135,
                    Height = 135,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = ConvertByteArrayToImage(staffMember.Image)
                };

                border.Child = image;
                staffPanel.Children.Add(border);

                staffPanel.Children.Add(new TextBlock
                {
                    Text = $"{staffMember.Last_name} {staffMember.First_name}",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 5)
                });

                var roleName = context.Role
                    .Where(r => r.ID_Role == staffMember.ID_Role)
                    .Select(r => r.roll_name)
                    .FirstOrDefault();

                staffPanel.Children.Add(new TextBlock
                {
                    Text = roleName ?? "Неизвестная должность",
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold
                });

                staffPanel.Children.Add(new TextBlock
                {
                    FontSize = 12,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 0)
                });

                uniformGridStaff.Children.Add(staffPanel);
            }
        }

        private BitmapImage ConvertByteArrayToImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            var bitmapImage = new BitmapImage();
            using (var stream = new MemoryStream(imageBytes))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }


        private void Button_Click_Authorizathion(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Visibility = Visibility.Visible;
            this.Close();
        }


        private void Button_Click_Service(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            service.Show();
            this.Visibility=Visibility.Visible;
            this.Close();
        }


        private void Button_Click_Contact(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Visibility = Visibility.Visible;
            this.Close();
        }


    }
}
