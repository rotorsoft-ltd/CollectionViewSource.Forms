using System;

namespace Rotorsoft.Forms
{
    internal class DeferHelper : IDisposable
    {
        private CollectionView _collectionView;

        public DeferHelper(CollectionView collectionView)
        {
            _collectionView = collectionView;
        }

        public void Dispose()
        {
            if (_collectionView != null)
            {
                _collectionView.EndDefer();
                _collectionView = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
