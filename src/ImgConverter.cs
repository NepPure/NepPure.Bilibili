using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NepPure.Bilibili
{
    public class ImgConverter : IValueConverter
    {
        private static ConcurrentDictionary<string, BitmapImage> _imgageCache = new ConcurrentDictionary<string, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var key = value.ToString();
                if (_imgageCache.TryGetValue(key, out BitmapImage image))
                {
                    return image;
                }

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(value.ToString(), UriKind.Absolute);
                bi.EndInit();

                _imgageCache.AddOrUpdate(key, bi, (ke, val) => bi);
                return bi;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }



    }
}
