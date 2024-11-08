using EuroBuld.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
		EuroBuldEntities5 _context;
		private byte[] _Image;
		public AdminPage()
        {
            InitializeComponent();
			_context = new EuroBuldEntities5();
		}

		private void LoadImage_Click(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog
			{
				Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
				Title = "Выберите изображение для машины"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				_Image = File.ReadAllBytes(openFileDialog.FileName);
				ImageFileNameTextBlock.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
			}
		}

		private async void Add_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(Item_NameTextBox.Text) ||
				string.IsNullOrWhiteSpace(Item_DesTextBox.Text) ||
				string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
				_Image == null)
			{
				MessageBox.Show("Пожалуйста, заполните все поля и выберите изображение.");
				return;
			}

			var newService = new EuroBuld.DataBase.Service
			{
				Item_Name = Item_NameTextBox.Text,
				Item_Description =Item_DesTextBox.Text,
				Price = PriceTextBox.Text,
				Image = _Image
			};

			try
			{
				_context.Service.Add(newService);
				_context.SaveChangesAsync();
				MessageBox.Show("добавлена успешно!");
				
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при добавлении машины: {ex.Message}");
			}
		}

		private void ClearFields()
		{
			Item_NameTextBox.Clear();
			Item_DesTextBox.Clear();
			PriceTextBox.Clear();
			_Image = null;
			ImageFileNameTextBlock.Text = string.Empty;
		}

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
			AddUser addUser = new AddUser();
			addUser.Show();
			this.Visibility = Visibility.Visible;
        }
    }

}
