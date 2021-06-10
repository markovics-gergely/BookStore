using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace BookStore.CLIENT.common
{
    public class NullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        { return value; }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            int temp;
            if (string.IsNullOrEmpty((string)value) || !int.TryParse((string)value, out temp)) return null;
            else return temp;
        }
    }
}
