using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfBudgedAccounting
{
    internal class MyMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool returned = false;
            foreach (var item in values)
            {
                string text = item.ToString();
                returned = !string.IsNullOrWhiteSpace(text);
                if (string.IsNullOrWhiteSpace(text))
                {
                    returned = false;
                    break;
                }
            }
            return returned;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
