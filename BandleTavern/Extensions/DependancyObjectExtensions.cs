using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace BandleTavern.Extensions
{
    public static class DependancyObjectExtensions
    {
        /// <summary>
        /// Finds first ancestor of a specific object type.
        /// </summary>
        /// <typeparam name="T">Object Type to search for</typeparam>
        /// <param name="dependencyObject">Child to begin search from.</param>
        /// <returns></returns>
        public static T FindAncestor<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }
    }
}
