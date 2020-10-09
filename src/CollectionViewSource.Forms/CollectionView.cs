using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Rotorsoft.Forms
{
    internal class CollectionView : ICollectionView
    {
        private int _deferLevel;
        private bool _needsRefresh;

        private Predicate<object> _filter;
        private ObservableCollection<SortDescription> _sortDescriptions;

        public CollectionView(IEnumerable collection)
        {
            SourceCollection = collection;
            SortDescriptions = new ObservableCollection<SortDescription>();
        }

        public virtual bool CanFilter => true;

        public virtual bool CanGroup => false;

        public virtual bool CanSort => false;

        public virtual Predicate<object> Filter
        {
            get => _filter;
            set
            {
                if (!CanFilter)
                {
                    throw new NotSupportedException();
                }

                _filter = value;

                RefreshOrDefer();
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (SourceCollection is ICollection collection)
                {
                    return collection.Count == 0;
                }

                var enumerator = GetEnumerator();
                var empty = !enumerator.MoveNext();

                if (enumerator is IDisposable disposableEnumerator)
                {
                    disposableEnumerator.Dispose();
                }

                return empty;
            }
        }

        public virtual ObservableCollection<SortDescription> SortDescriptions
        {
            get => _sortDescriptions;
            private set
            {
                if (_sortDescriptions != null)
                {
                    _sortDescriptions.CollectionChanged -= SortDescriptions_CollectionChanged;
                }

                _sortDescriptions = value;

                if (_sortDescriptions != null)
                {
                    _sortDescriptions.CollectionChanged += SortDescriptions_CollectionChanged;
                }
            }
        }

        private void SortDescriptions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!CanSort)
            {
                throw new NotSupportedException();
            }

            if (e.NewItems != null)
            {
                foreach (SortDescription newItem in e.NewItems)
                {
                    newItem.Seal();
                }
            }

            RefreshOrDefer();
        }

        public virtual IEnumerable SourceCollection { get; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public virtual bool Contains(object item)
        {
            var index = -1;
            var i = 0;
            var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (object.Equals(enumerator.Current, item))
                {
                    index = i;

                    break;
                }

                ++i;
            }

            if (enumerator is IDisposable disposableEnumerator)
            {
                disposableEnumerator.Dispose();
            }

            return index >= 0;
        }

        public virtual IDisposable DeferRefresh()
        {
            ++_deferLevel;

            return new DeferHelper(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Refresh()
        {
            RefreshInternal();
        }

        protected virtual IEnumerator GetEnumerator()
        {
            return new FilteredEnumerator(SourceCollection, Filter);
        }

        internal void RefreshInternal()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            _needsRefresh = false;
        }

        internal void EndDefer()
        {
            --_deferLevel;

            if (_deferLevel == 0 && _needsRefresh)
            {
                Refresh();
            }
        }

        protected void RefreshOrDefer()
        {
            if (_deferLevel > 0)
            {
                _needsRefresh = true;
            }
            else
            {
                RefreshInternal();
            }
        }
    }
}
