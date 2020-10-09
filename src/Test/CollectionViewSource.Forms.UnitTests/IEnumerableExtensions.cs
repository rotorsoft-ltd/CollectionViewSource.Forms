using System;
using System.Collections;

namespace CollectionViewSource.Forms.UnitTests
{
    internal static class IEnumerableExtensions
    {
        public static int Count(this IEnumerable enumerable)
        {
            var count = 0;
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }

            if (enumerator is IDisposable disposable)
            {
                disposable.Dispose();
            }

            return count;
        }
    }
}
