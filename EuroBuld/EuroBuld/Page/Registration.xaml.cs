using EuroBuld.DataBase;
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

namespace EuroBuld.Page
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void Button_Click_Authorization(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Visibility = Visibility.Collapsed;
            this.Close();
        }


        private void Button_Click_Accept(object sender, MouseButtonEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "PersonalDataProcessingAgreement.doc");

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось открыть документ: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Файл с политикой обработки персональных данных не найден.");
            }
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AcceptCheckBox.IsChecked != true)
            {
                MessageBox.Show("Вы должны согласиться на обработку персональных данных.");
                return;
            }

            string email = EmailTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string repeatPassword = RepeatPasswordTextBox.Text.Trim();

            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают. Пожалуйста, повторите ввод.");
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Введите правильный email.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Введите корректный email-адрес.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль.");
                return;
            }

            using (var context = new EuroBuldEntities16())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким email уже существует.");
                    return;
                }

                var newUser = new Users
                {
                    Email = email,
                    Password = password,
                };


                string verificationCode = GenerateVerificationCode();

                SendVerificationEmail(email, verificationCode);

                VerifyCodeWindow verifyCodeWindow = new VerifyCodeWindow(verificationCode, email, password);
                verifyCodeWindow.ShowDialog();

                this.Close();
            }
        }


        private string GenerateVerificationCode()
        {
            Random random = new Random();
            string code = random.Next(100000, 999999).ToString(); 
            return code;
        }


        private void SendVerificationEmail(string toEmail, string verificationCode)
        {
            string subject = "Код подтверждения регистрации";
            string body = $"Ваш код подтверждения: {verificationCode}";

            try
            {
                using (var context = new EuroBuldEntities16())
                {
                    var senderInfo = context.Staff
                                    .Join(context.Role,
                                          staff => staff.ID_Role,
                                          role => role.ID_Role,
                                          (staff, role) => new { staff, role })
                                    .FirstOrDefault(sr => sr.role.roll_name == "SendCheck");

                    if (senderInfo == null)
                    {
                        MessageBox.Show("Данные отправителя не найдены в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string senderEmail = senderInfo.staff.Email;
                    string senderPassword = "jlyx djso obwj vkvu";

                    using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtpClient.EnableSsl = true;

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress(senderEmail, "EuroBuld"),
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = false
                        };

                        mailMessage.To.Add(toEmail);

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке письма: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
