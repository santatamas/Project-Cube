using System;
using System.Windows.Data;
using System.Windows.Media;

namespace PixelMatrixEditor.Converters
{
    public class ByteToColorConverter : IValueConverter
    {
        private Color _pixelOnBrush = Color.FromRgb(53, 53, 53);
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SolidColorBrush(Color.FromArgb((byte)value, _pixelOnBrush.R, _pixelOnBrush.G, _pixelOnBrush.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
