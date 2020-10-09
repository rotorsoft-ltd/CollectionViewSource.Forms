using System;
using System.ComponentModel;

namespace Rotorsoft.Forms
{
    public class SortDescription
    {
        private string _propertyName;
        private ListSortDirection _direction;

        public SortDescription(string propertyName, ListSortDirection direction)
        {
            if (direction != ListSortDirection.Ascending && direction != ListSortDirection.Descending)
            {
                throw new InvalidEnumArgumentException(nameof(direction), (int)direction, typeof(ListSortDirection));
            }

            _propertyName = propertyName;
            _direction = direction;
            IsSealed = false;
        }

        public string PropertyName
        {
            get => _propertyName;
            set
            {
                if (IsSealed)
                {
                    throw new InvalidOperationException("Cannot change SortDescription after it has been sealed.");
                }

                _propertyName = value;
            }
        }

        public ListSortDirection Direction
        {
            get => _direction;
            set
            {
                if (IsSealed)
                {
                    throw new InvalidOperationException("Cannot change SortDescription after it has been sealed.");
                }

                if (value != ListSortDirection.Ascending && value != ListSortDirection.Descending)
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ListSortDirection));
                }

                _direction = value;
            }
        }

        public bool IsSealed
        {
            get;
            private set;
        }

        internal void Seal()
        {
            IsSealed = true;
        }
    }
}
