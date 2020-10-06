using System;
using System.Collections;
using System.Collections.Specialized;

namespace Rotorsoft.Forms
{
    internal class CollectionView : ICollectionView
    {
        private Predicate<object> _filter;

        public CollectionView(IEnumerable collection)
        {
            SourceCollection = collection;
        }

        public bool CanFilter => true;

        public bool CanGroup => throw new NotImplementedException();

        public bool CanSort => throw new NotImplementedException();

        public Predicate<object> Filter
        {
            get => _filter;
            set
            {
                if (!CanFilter)
                {
                    throw new NotSupportedException();
                }

                _filter = value;

                Refresh();
            }
        }

        public bool IsEmpty => throw new NotImplementedException();

        public IEnumerable SourceCollection { get; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public bool Contains(object item)
        {
            throw new NotImplementedException();
        }

        public IDisposable DeferRefresh()
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return new FilteredEnumerator(SourceCollection, Filter);
        }

        public void Refresh()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
