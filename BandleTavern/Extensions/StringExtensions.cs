using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BandleTavern.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves the image located at a string uri. Returns null if error encountered.
        /// </summary>
        /// <param name="s">String Uri</param>
        /// <returns></returns>
        public static ImageSource StringToImageSource(this string s)
        {
            BitmapImage imageBitmap = null;
            try
            {
                Uri imageUri = new Uri(s, UriKind.Absolute);
                imageBitmap = new BitmapImage(imageUri);
            }
            catch{

            }
            return imageBitmap;
        }
    }
}
