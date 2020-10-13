using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rotorsoft.Forms
{
    internal class SortFieldComparer : IComparer
    {
        private SortPropertyInfo[] _properties;

        public SortFieldComparer(IList<SortDescription> sortDescriptions)
        {
            _properties = CreatePropertyInfo(sortDescriptions);
        }

        public int Compare(object x, object y)
        {
            var result = 0;

            for (var i = 0; i < _properties.Length; ++i)
            {
                var value1 = _properties[i].GetValue(x);
                var value2 = _properties[i].GetValue(y);

                result = Comparer.DefaultInvariant.Compare(value1, value2);

                if (_properties[i].IsDescending)
                {
                    result = -result;
                }

                if (result != 0)
                {
                    break;
                }
            }

            return result;
        }

        private static SortPropertyInfo[] CreatePropertyInfo(IList<SortDescription> sortDescriptions)
        {
            var properties = new SortPropertyInfo[sortDescriptions.Count];

            for (var i = 0; i < sortDescriptions.Count; ++i)
            {
                properties[i].PropertyName = sortDescriptions[i].PropertyName;
                properties[i].IsDescending = (sortDescriptions[i].Direction == ListSortDirection.Descending);
            }

            return properties;
        }
    }
}
