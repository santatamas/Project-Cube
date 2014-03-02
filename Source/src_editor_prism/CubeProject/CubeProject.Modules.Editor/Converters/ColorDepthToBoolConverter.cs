using System;
using System.Globalization;
using System.Windows.Data;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Modules.Editor.Converters
{
    public class ColorDepthToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ColorDepth) value) == ColorDepth.GrayScale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
