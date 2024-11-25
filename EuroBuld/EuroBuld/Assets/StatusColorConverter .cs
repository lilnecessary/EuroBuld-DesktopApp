using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EuroBuld.Assets
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                // Убедимся, что статус корректен
                Console.WriteLine($"Статус: {status}");

                switch (status.Trim().ToLower())
                {
                    case "выполнен":
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c7f9c3"));
                    case "в процессе":
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ebb586"));
                    case "принят":
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c3eff9"));
                    case "отменен":
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fa8f8f"));
                    default:
                        return new SolidColorBrush(Colors.Gray); // Фон по умолчанию
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
