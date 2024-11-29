using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using EuroBuld.DataBase;
using System.ComponentModel;

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для VerifyCodeWindow.xaml
    /// </summary>
        public partial class VerifyCodeWindow : Window
        {
            private readonly string _verificationCode;
            private readonly string _email;
            private readonly string _password;

            public VerifyCodeWindow(string verificationCode, string email, string password)
            {
                InitializeComponent();
                _verificationCode = verificationCode;
                _email = email;
                _password = password;
            }


            private void RegisterButton_Click(object sender, RoutedEventArgs e)
            {
                string enteredCode = VerificationCodeTextBox.Text.Trim();

                if (enteredCode == _verificationCode)
                {
                    RegisterUser();

                    MessageBox.Show("Регистрация успешна!");
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    this.Close();
            }
            else
                {
                    MessageBox.Show("Неверный код подтверждения.");
                }
            }


            private void RegisterUser()
            {
                using (var context = new EuroBuldEntities15())
                {
                    var newUser = new Users { Email = _email, Password = _password };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
            }


            protected override void OnClosing(CancelEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(VerificationCodeTextBox.Text))
                {
                    e.Cancel = true;
                }
                base.OnClosing(e);
            }
        }
    }

