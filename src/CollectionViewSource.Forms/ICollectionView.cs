using System;
using System.Collections;
using System.Collections.Specialized;

namespace Rotorsoft.Forms
{
    public interface ICollectionView : IEnumerable, INotifyCollectionChanged
    {
        /// <summary>
        /// Gets a value that indicates whether this view supports filtering via the <see cref="Filter"/> property.
        /// </summary>
        /// <value><c>true</c> if this view support filtering; otherwise, <c>false</c>.</value>
        bool CanFilter { get; }

        /// <summary>
        /// Gets a value that indicates whether this view supports grouping via the <see cref="GroupDescriptions"/> property.
        /// </summary>
        /// <value><c>true</c> if this view supports grouping; otherwise, <c>false</c>.</value>
        bool CanGroup { get; }

        /// <summary>
        /// Gets a value that indicates whether this view supports sorting via the <see cref="SortDescriptions"/> property.
        /// </summary>
        /// <value><c>true</c> if this view supports sorting; otherwise, <c>false</c>.</value>
        bool CanSort { get; }

        /// <summary>
        /// Gets or sets a callback used to determine if an item is suitable for inclusion in the view.
        /// </summary>
        /// <value>A method used to determine if an item is suitable for inclusion in the view.</value>
        Predicate<object> Filter { get; set; }

        /// <summary>
        /// Returns a value that indicates whether the resulting view is empty.
        /// </summary>
        /// <value><c>true</c> if the resulting view is empty; otherwise, <c>false</c>.</value>
        bool IsEmpty { get; }

        /// <summary>
        /// Returns the underlying collection.
        /// </summary>
        /// <value>An <see cref="System.Collections.IEnumerable"/> object that is the underlying collection.</value>
        IEnumerable SourceCollection { get; }

        /// <summary>
        /// Returns a value that indicates whether a given item belongs to this collection view.
        /// </summary>
        /// <param name="item">The object to check.</param>
        /// <returns><c>true</c> if the item belongs to this collection view; otherwise, <c>false</c>.</returns>
        /// <remarks>This method does not make any assumptions about whether the item belongs to the underlying collection.</remarks>
        bool Contains(object item);

        /// <summary>
        /// Enters a defer cycle that you can use to merge changes to the view and delay automatic refresh.
        /// </summary>
        /// <returns>An <see cref="System.IDisposable"/> object that you can use to dispose of the calling object.</returns>
        IDisposable DeferRefresh();

        /// <summary>
        /// Recreates the view.
        /// </summary>
        void Refresh();
    }
}
