using System;
using System.Collections;
using System.Collections.Specialized;

namespace Rotorsoft.Forms
{
    internal class ListCollectionView : CollectionView, IComparer, IDisposable
    {
        private IList _list;
        private ArrayList _shadowCollection;
        private IComparer _activeComparer;
        private bool _disposedValue;

        public ListCollectionView(IList list)
            : base(list)
        {
            _list = list;
            _shadowCollection = new ArrayList(_list);

            if (_list is INotifyCollectionChanged collectionChanged)
            {
                collectionChanged.CollectionChanged += ShadowCollection_CollectionChanged;
            }
        }

        private void ShadowCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    {
                        _shadowCollection.Clear();

                        RefreshOrDefer();
                    }
                    break;

                case NotifyCollectionChangedAction.Add:
                    {
                        _shadowCollection.AddRange(e.NewItems);

                        RefreshOrDefer();
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        for (var i = 0; i < e.OldItems.Count; ++i)
                        {
                            _shadowCollection.Remove(e.OldItems[i]);
                        }

                        RefreshOrDefer();
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        for (var i = 0; i < e.OldItems.Count; ++i)
                        {
                            _shadowCollection.Remove(e.OldItems[i]);
                        }

                        _shadowCollection.AddRange(e.NewItems);

                        RefreshOrDefer();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        RefreshOrDefer();
                    }
                    break;
            }
        }

        public override bool CanFilter => true;

        public override bool CanSort => true;

        public override bool CanGroup => false;

        public int Compare(object x, object y)
        {
            if (_activeComparer != null)
            {
                return _activeComparer.Compare(x, y);
            }

            var index1 = _shadowCollection.IndexOf(x);
            var index2 = _shadowCollection.IndexOf(y);

            return (index1 - index2);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected override IEnumerator GetEnumerator()
        {
            return new FilteredEnumerator(_shadowCollection, Filter);
        }

        protected override void RefreshOverride()
        {
            _activeComparer = null;

            if (SortDescriptions?.Count > 0)
            {
                _activeComparer = new SortFieldComparer(SortDescriptions);
            }

            _shadowCollection.Sort(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_list is INotifyCollectionChanged collectionChanged)
                    {
                        collectionChanged.CollectionChanged -= ShadowCollection_CollectionChanged;
                    }
                }

                _disposedValue = true;
            }
        }
    }
}
