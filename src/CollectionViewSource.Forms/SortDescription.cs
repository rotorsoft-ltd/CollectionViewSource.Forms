using System.ComponentModel;

namespace Rotorsoft.Forms
{
    public struct SortDescription
    {
        private string _propertyName;
        private ListSortDirection _direction;

        public SortDescription(string propertyName, ListSortDirection direction)
        {
            if (direction != ListSortDirection.Ascending && direction != ListSortDirection.Descending)
            {
                throw new InvalidEnumArgumentException(nameof(direction), (int)direction, typeof(ListSortDirection));
            }

            _direction = direction;
            _propertyName = propertyName;
        }

        public string PropertyName
        {
            get => _propertyName;
            set => _propertyName = value;
        }

        public ListSortDirection Direction
        {
            get => _direction;
            set
            {
                if (value != ListSortDirection.Ascending && value != ListSortDirection.Descending)
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ListSortDirection));
                }

                _direction = value;
            }
        }
    }
}
