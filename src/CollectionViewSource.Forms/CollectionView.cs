using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rotorsoft.Forms
{
    internal class CollectionView : ObservableCollection<object>, ICollectionView
    {
        public CollectionView(IEnumerable<object> collection) :
            base(collection)
        {
        }
    }
}
