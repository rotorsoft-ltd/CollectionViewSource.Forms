using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Rotorsoft.Forms
{
    public class CollectionViewSource : BindableObject
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(IEnumerable<object>),
            typeof(CollectionViewSource),
            defaultValue: null,
            propertyChanged: OnSourceChanged);

        public static BindableProperty FilterProperty = BindableProperty.Create(
            nameof(Filter),
            typeof(Predicate<object>),
            typeof(CollectionViewSource),
            defaultValue: null,
            propertyChanged: OnFilterChanged);

        public static BindableProperty ViewProperty = BindableProperty.Create(
            nameof(View),
            typeof(ICollectionView),
            typeof(CollectionViewSource),
            defaultValue: null,
            propertyChanged: OnViewChanged);

        public CollectionViewSource()
        {
            SortDescriptions = new SortDescriptionCollection();
        }

        public IEnumerable<object> Source
        {
            get => (IEnumerable<object>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public Predicate<object> Filter
        {
            get => (Predicate<object>)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public ICollectionView View
        {
            get => (ICollectionView)GetValue(ViewProperty);
            private set => SetValue(ViewProperty, value);
        }

        public SortDescriptionCollection SortDescriptions { get; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnFilterChanged(Predicate<object> oldValue, Predicate<object> newValue)
        {
            if (View != null)
            {
                View.Filter = newValue;
            }
        }

        private void OnSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            ICollectionView newView = null; 

            if (newValue != null)
            {
                newView = new CollectionView(newValue);
                // TODO: Deferred set of all properties
                newView.Filter = Filter;
            }

            View = newView;
        }

        private void OnViewChanged(ICollectionView oldValue, ICollectionView newValue)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private static void OnFilterChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var collectionViewSource = bindableObject as CollectionViewSource;

            collectionViewSource?.OnFilterChanged(oldValue as Predicate<object>, newValue as Predicate<object>);
        }

        private static void OnSourceChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var collectionViewSource = bindableObject as CollectionViewSource;

            collectionViewSource?.OnSourceChanged(oldValue as IEnumerable, newValue as IEnumerable);
        }

        private static void OnViewChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var collectionViewSource = bindableObject as CollectionViewSource;

            collectionViewSource?.OnViewChanged(oldValue as ICollectionView, newValue as ICollectionView);
        }
    }
}
