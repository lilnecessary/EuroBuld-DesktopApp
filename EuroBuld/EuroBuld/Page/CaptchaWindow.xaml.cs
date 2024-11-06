using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace EuroBuld.Page
{
    public partial class CaptchaWindow : Window
    {
        private const string SiteKey = "6LcST3cqAAAAAGdvRuZAgyeW0esoB8HnvpgdB0Lr";
        private const string SecretKey = "6LcST3cqAAAAAOrxv9cBVT2UnKrwvmVakrxkNaJv";
        public bool IsCaptchaVerified { get; private set; }

        public CaptchaWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await RecaptchaBrowser.EnsureCoreWebView2Async();
            LoadRecaptcha();
        }

        private void LoadRecaptcha()
        {
            string html = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <script src='https://www.google.com/recaptcha/api.js'></script>
        </head>
        <body>
            <div class='g-recaptcha' data-sitekey='{SiteKey}' data-callback='onRecaptchaSuccess'></div>
            <script>
                function onRecaptchaSuccess(token) {{
                    window.chrome.webview.postMessage(token);
                }}
            </script>
        </body>
        </html>";

            RecaptchaBrowser.NavigateToString(html);
            RecaptchaBrowser.WebMessageReceived += RecaptchaBrowser_WebMessageReceived;
        }

        private async void RecaptchaBrowser_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var token = e.TryGetWebMessageAsString();
            if (await VerifyRecaptchaTokenAsync(token))
            {
                IsCaptchaVerified = true;
                this.DialogResult = true; // Закрываем окно
            }
            else
            {
                MessageBox.Show("Проверка reCAPTCHA не пройдена. Попробуйте снова.");
                IsCaptchaVerified = false;
            }
        }

        private async Task<bool> VerifyRecaptchaTokenAsync(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={token}", null);
                var json = await response.Content.ReadAsStringAsync();
                return json.Contains("\"success\": true");
            }
        }
    }

}
