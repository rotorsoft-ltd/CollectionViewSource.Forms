using System.Collections.Generic;
using System.Collections.Specialized;

namespace Rotorsoft.Forms
{
    public interface ICollectionView : IEnumerable<object>, IList<object>, INotifyCollectionChanged
    {
    }
}
