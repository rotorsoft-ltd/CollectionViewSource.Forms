namespace Rotorsoft.Forms
{
    internal struct SortPropertyInfo
    {
        public string PropertyName;
        public bool IsDescending;

        public object GetValue(object o)
        {
            if (string.IsNullOrWhiteSpace(PropertyName))
            {
                return o;
            }

            return o.GetType().GetProperty(PropertyName).GetValue(o);
        }
    }
}
