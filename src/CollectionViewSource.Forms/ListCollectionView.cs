using System.Collections;

namespace Rotorsoft.Forms
{
    internal class ListCollectionView : CollectionView, IComparer
    {
        private IList _internalList;

        public ListCollectionView(IList list)
            : base(list)
        {
            _internalList = list;
        }

        public override bool CanFilter => true;

        public override bool CanSort => true;

        public override bool CanGroup => false;

        public int Compare(object x, object y)
        {
            throw new System.NotImplementedException();
        }
    }
}
