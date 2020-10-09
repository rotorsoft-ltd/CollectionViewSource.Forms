using System.Reflection;

namespace Rotorsoft.Forms
{
    internal struct SortPropertyInfo
    {
        public int Index;
        public PropertyInfo Info;
        public bool IsDescending;

        public object GetValue(object o)
        {
            return Info.GetValue(o);
        }
    }
}
