using System;
using System.Collections;

namespace Rotorsoft.Forms
{
    internal class FilteredEnumerator : IEnumerator, IDisposable
    {
        private IEnumerable _enumerable;
        private IEnumerator _enumerator;
        private Predicate<object> _filterCallback;

        public FilteredEnumerator(IEnumerable enumerable, Predicate<object> filterCallback)
        {
            _enumerable = enumerable;
            _enumerator = _enumerable.GetEnumerator();
            _filterCallback = filterCallback;
        }

        public object Current => _enumerator.Current;

        public bool MoveNext()
        {
            var returnValue = false;

            if (_filterCallback == null)
            {
                returnValue = _enumerator.MoveNext();
            }
            else
            {
                while ((returnValue = _enumerator.MoveNext()) && !_filterCallback(_enumerator.Current)) ;
            }

            return returnValue;
        }

        public void Reset()
        {
            Dispose();

            _enumerator = _enumerable.GetEnumerator();
        }

        public void Dispose()
        {
            var disposable = _enumerator as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            _enumerator = null;
        }
    }
}
