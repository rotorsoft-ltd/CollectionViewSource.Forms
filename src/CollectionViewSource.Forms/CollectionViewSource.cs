using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            defaultValue: null);

        public static BindableProperty SortDescriptionsProperty = BindableProperty.Create(
            nameof(SortDescriptions),
            typeof(ObservableCollection<SortDescription>),
            typeof(CollectionViewSource),
            defaultValue: null,
            propertyChanged: OnSortDescriptionsChanged);

        public CollectionViewSource()
        {
            SortDescriptions = new ObservableCollection<SortDescription>();
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

        public ObservableCollection<SortDescription> SortDescriptions
        {
            get => (ObservableCollection<SortDescription>)GetValue(SortDescriptionsProperty);
            set => SetValue(SortDescriptionsProperty, value);
        }

        private void ApplyPropertiesToView(ICollectionView collectionView)
        {
            if (collectionView == null)
            {
                return;
            }

            using (var deferral = collectionView.DeferRefresh())
            {
                if (!collectionView.CanFilter)
                {
                    if (Filter != null)
                    {
                        throw new InvalidOperationException("Current CollectionView does not support filtering.");
                    }
                }
                else
                {
                    collectionView.Filter = Filter;
                }

                if (!collectionView.CanSort)
                {
                    if (SortDescriptions != null &&
                        SortDescriptions.Count > 0)
                    {
                        throw new InvalidOperationException("Current CollectionView does not support sorting.");
                    }
                }
                else
                {
                    collectionView.SortDescriptions.Clear();

                    if (SortDescriptions != null)
                    {
                        for (int i = 0; i < SortDescriptions.Count; ++i)
                        {
                            collectionView.SortDescriptions.Add(SortDescriptions[i]);
                        }
                    }
                }
            }
        }

        private void OnFilterChanged(Predicate<object> oldValue, Predicate<object> newValue)
        {
            ApplyPropertiesToView(View);
        }

        private void OnSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            ICollectionView newView = null;

            if (newValue != null)
            {
                if (newValue is IList newList)
                {
                    newView = new ListCollectionView(newList);
                }
                else
                { 
                    newView = new CollectionView(newValue);
                }

                ApplyPropertiesToView(newView);
            }

            View = newView;
        }

        private void OnSortDescriptionsChanged(ObservableCollection<SortDescription> oldValue, ObservableCollection<SortDescription> newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= SortDescriptions_CollectionChanged;
            }

            if (View != null)
            {
                ApplyPropertiesToView(View);
            }

            if (newValue != null)
            {
                newValue.CollectionChanged += SortDescriptions_CollectionChanged;
            }
        }

        private void SortDescriptions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ApplyPropertiesToView(View);
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

        private static void OnSortDescriptionsChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var collectionViewSource = bindableObject as CollectionViewSource;

            collectionViewSource?.OnSortDescriptionsChanged(oldValue as ObservableCollection<SortDescription>, newValue as ObservableCollection<SortDescription>);
        }
    }
}
