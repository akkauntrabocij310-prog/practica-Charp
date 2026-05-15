using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentDiary.Converters
{
    /// <summary>
    /// Конвертирует числовую оценку (1–5) в цвет для визуального выделения.
    /// </summary>
    public class GradeValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int grade)
            {
                return grade switch
                {
                    5 => new SolidColorBrush(Color.FromRgb(39, 174, 96)),   // зелёный
                    4 => new SolidColorBrush(Color.FromRgb(41, 128, 185)),  // синий
                    3 => new SolidColorBrush(Color.FromRgb(243, 156, 18)),  // жёлтый
                    _ => new SolidColorBrush(Color.FromRgb(231, 76, 60)),   // красный (1, 2)
                };
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    /// <summary>
    /// Конвертирует bool IsLoading в Visibility.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = parameter?.ToString() == "Invert";
            bool visible = value is bool b && b;
            if (invert) visible = !visible;
            return visible
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
