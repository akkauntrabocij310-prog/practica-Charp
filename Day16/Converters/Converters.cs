using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentDiaryFull.Converters
{
    /// <summary>Оценка → цвет фона.</summary>
    public class GradeValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int grade)
                return grade switch
                {
                    5 => new SolidColorBrush(Color.FromRgb(39,  174, 96)),
                    4 => new SolidColorBrush(Color.FromRgb(41,  128, 185)),
                    3 => new SolidColorBrush(Color.FromRgb(243, 156, 18)),
                    _ => new SolidColorBrush(Color.FromRgb(231, 76,  60)),
                };
            return Brushes.Gray;
        }
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }

    /// <summary>bool → Visibility (с поддержкой инверсии через ConverterParameter=Invert).</summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert  = parameter?.ToString() == "Invert";
            bool visible = value is bool b && b;
            if (invert) visible = !visible;
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }

    /// <summary>null/не-null → Visibility.</summary>
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert  = parameter?.ToString() == "Invert";
            bool notNull = value != null;
            bool visible = invert ? !notNull : notNull;
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }

    /// <summary>Выполнено ДЗ → зачёркнутый стиль.</summary>
    public class BoolToStrikethroughConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b && b ? TextDecorations.Strikethrough : null!;
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }

    /// <summary>Выполнено ДЗ → текст кнопки.</summary>
    public class BoolToToggleTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b && b ? "✓ Выполнено" : "○ Не выполнено";
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }

    /// <summary>Выполнено ДЗ → цвет строки.</summary>
    public class BoolToCompletedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b && b
                ? new SolidColorBrush(Color.FromRgb(149, 165, 166))
                : new SolidColorBrush(Color.FromRgb(44, 62, 80));
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }
}
