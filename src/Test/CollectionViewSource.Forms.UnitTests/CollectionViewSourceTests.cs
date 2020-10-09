using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CollectionViewSource.Forms.UnitTests
{
    public class CollectionViewSourceTests
    {
        [Fact]
        public void Constructor()
        {
            var cvs = new Rotorsoft.Forms.CollectionViewSource();

            Assert.Null(cvs.Source);
            Assert.Null(cvs.View);
            Assert.Null(cvs.Filter);
            Assert.Empty(cvs.SortDescriptions);
        }

        [Fact]
        public void SourceWithEmptyEnumerable()
        {
            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = new TestEnumerable();

            Assert.NotNull(cvs.Source);
            Assert.Empty(cvs.Source);
            Assert.NotNull(cvs.View);
            Assert.Empty(cvs.View);

            Assert.True(cvs.View.CanFilter);
            Assert.False(cvs.View.CanSort);
            Assert.False(cvs.View.CanGroup);
        }

        [Fact]
        public void SourceWithEmptyList()
        {
            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = new List<object>();

            Assert.NotNull(cvs.Source);
            Assert.Empty(cvs.Source);
            Assert.NotNull(cvs.View);
            Assert.Empty(cvs.View);

            Assert.True(cvs.View.CanFilter);
            Assert.True(cvs.View.CanSort);
            Assert.False(cvs.View.CanGroup);
        }

        [Fact]
        public void SourceWithNonEmptyEnumerable()
        {
            var items = new TestEnumerable(new object[]
            {
                "Lorem Ipsum"
            });

            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = items;

            Assert.Same(items, cvs.Source);
            Assert.NotNull(cvs.View);
            Assert.NotEmpty(cvs.View);
        }

        [Fact]
        public void SourceWithNonEmptyList()
        {
            var items = new List<object>()
            {
                "Lorem Ipsum"
            };

            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = items;

            Assert.Same(items, cvs.Source);
            Assert.NotNull(cvs.View);
            Assert.NotEmpty(cvs.View);
        }

        [Fact]
        public void FilterWithEnumerable()
        {
            var items = new TestEnumerable(new object[]
            {
                10,
                "Lorem Ipsum",
                TimeSpan.Zero,
            });

            Predicate<object> filterPredicate = (item) => item is string;

            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = items;
            cvs.Filter = filterPredicate;

            Assert.Same(filterPredicate, cvs.Filter);
            Assert.NotNull(cvs.View);
            Assert.NotEmpty(cvs.View);
            Assert.Equal(1, cvs.View.Count());
        }

        [Fact]
        public void FilterWithList()
        {
            var items = new List<object>()
            {
                10,
                "Lorem Ipsum",
                TimeSpan.Zero,
            };

            Predicate<object> filterPredicate = (item) => item is string;

            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = items;
            cvs.Filter = filterPredicate;

            Assert.Same(filterPredicate, cvs.Filter);
            Assert.NotNull(cvs.View);
            Assert.NotEmpty(cvs.View);
            Assert.Equal(1, cvs.View.Count());
        }

        [Fact]
        public void RemoveFilter()
        {
            var items = new List<object>()
            {
                10,
                "Lorem Ipsum",
                TimeSpan.Zero,
            };

            Predicate<object> filterPredicate = (item) => item is string;

            var cvs = new Rotorsoft.Forms.CollectionViewSource();
            cvs.Source = items;
            cvs.Filter = filterPredicate;

            Assert.Equal(1, cvs.View.Count());

            cvs.Filter = null;

            Assert.Equal(3, cvs.View.Count());
        }
    }

    internal class TestEnumerable : IEnumerable<object>
    {
        private IList<object> _list;

        public TestEnumerable()
            : this(null)
        {
        }

        public TestEnumerable(IEnumerable<object> enumerable)
        {
            if (enumerable != null)
            {
                _list = new List<object>(enumerable);
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            if (_list == null)
            {
                return Enumerable.Empty<object>().GetEnumerator();
            }

            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class TestModel
    {
        public TestModel(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; }

        public int Score { get; }

    }
}
