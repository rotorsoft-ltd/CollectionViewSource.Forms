using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Rotorsoft.Forms
{
    public class SortDescriptionCollection : Collection<SortDescription>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void ClearItems()
        {
            base.ClearItems();
        }
    }
}
