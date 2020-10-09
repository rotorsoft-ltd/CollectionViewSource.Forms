using System.Collections;

namespace Rotorsoft.Forms
{
    internal class ListCollectionView : CollectionView, IComparer
    {
        private ArrayList _shadowCollection;

        public ListCollectionView(IList list)
            : base(list)
        {
            _shadowCollection = new ArrayList(list);
        }

        public override bool CanFilter => true;

        public override bool CanSort => true;

        public override bool CanGroup => false;

        public override void Refresh()
        {
            _shadowCollection.Sort(this);

            base.Refresh();
        }

        public int Compare(object x, object y)
        {
            if (ActiveComparer != null)
            {
                return ActiveComparer.Compare(x, y);
            }

            int i1 = _shadowCollection.IndexOf(x);
            int i2 = _shadowCollection.IndexOf(y);

            return (i1 - i2);
        }
    }
}
