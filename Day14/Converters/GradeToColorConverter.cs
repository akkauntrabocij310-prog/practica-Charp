using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentDiary.Converters
{
    public class GradeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double grade)
            {
                if (grade >= 4.5) return new SolidColorBrush(Color.FromRgb(34, 197, 94));   // green
                if (grade >= 3.5) return new SolidColorBrush(Color.FromRgb(234, 179, 8));   // yellow
                if (grade >= 2.5) return new SolidColorBrush(Color.FromRgb(249, 115, 22));  // orange
                return new SolidColorBrush(Color.FromRgb(239, 68, 68));                      // red
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class AverageGradeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double avg)
            {
                if (avg >= 4.5) return new SolidColorBrush(Color.FromRgb(34, 197, 94));
                if (avg >= 3.5) return new SolidColorBrush(Color.FromRgb(234, 179, 8));
                if (avg >= 2.5) return new SolidColorBrush(Color.FromRgb(249, 115, 22));
                return new SolidColorBrush(Color.FromRgb(239, 68, 68));
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
