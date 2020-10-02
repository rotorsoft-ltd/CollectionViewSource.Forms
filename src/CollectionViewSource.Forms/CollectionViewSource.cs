using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Rotorsoft.Forms
{
    public class CollectionViewSource : BindableObject
    {
        private SortDescriptionCollection _sortDescriptions;

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(IEnumerable<object>),
            typeof(CollectionViewSource),
            defaultValue: null,
            propertyChanged: OnSourceChanged);

        public static readonly BindableProperty ViewProperty = BindableProperty.Create(
            nameof(View),
            typeof(ICollectionView),
            typeof(CollectionViewSource),
            defaultValue: null);

        public static BindableProperty FilterDelegateProperty = BindableProperty.Create(
            nameof(FilterDelegate),
            typeof(Func<object, bool>),
            typeof(CollectionViewSource),
            defaultValue: null);

        public CollectionViewSource()
        {
            _sortDescriptions = new SortDescriptionCollection();
        }

        public IEnumerable<object> Source
        {
            get => (IEnumerable<object>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public ICollectionView View
        {
            get => (ICollectionView)GetValue(ViewProperty);
            private set => SetValue(ViewProperty, value);
        }

        public Func<object, bool> FilterDelegate
        {
            get => (Func<object, bool>)GetValue(FilterDelegateProperty);
            set => SetValue(FilterDelegateProperty, value);
        }

        public SortDescriptionCollection SortDescriptions => _sortDescriptions;

        private void OnSourceChanged(IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
            ICollectionView newView = null;

            if (newValue != null)
            {
                newView = new CollectionView(newValue);
            }

            View = newView;
        }

        private static void OnSourceChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var collectionViewSource = bindableObject as CollectionViewSource;

            collectionViewSource?.OnSourceChanged(oldValue as IEnumerable<object>, newValue as IEnumerable<object>);
        }
    }
}
