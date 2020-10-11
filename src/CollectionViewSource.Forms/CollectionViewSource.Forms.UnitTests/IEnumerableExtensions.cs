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

        public static T GetItemAt<T>(this IEnumerable enumerable, int index)
        {
            T foundValue = default(T);
            var position = -1;
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
            {
                ++position;

                if (position == index)
                {
                    foundValue = (T)enumerator.Current;

                    break;
                }
            }

            if (enumerator is IDisposable disposable)
            {
                disposable.Dispose();
            }

            return foundValue;
        }
    }
}
