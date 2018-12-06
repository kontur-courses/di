using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TagsCloudContainer
{
    public class FilterHashSet<T> : HashSet<T>
    {
        private readonly FilterType filterType;

        public FilterHashSet(FilterType filterType)
        {
            this.filterType = filterType;
        }

        public FilterHashSet(FilterType filterType, int capacity) : base(capacity)
        {
            this.filterType = filterType;
        }

        public FilterHashSet(FilterType filterType, IEqualityComparer<T> comparer) : base(comparer)
        {
            this.filterType = filterType;
        }

        public FilterHashSet(FilterType filterType, IEnumerable<T> collection) : base(collection)
        {
            this.filterType = filterType;
        }

        public FilterHashSet(FilterType filterType, IEnumerable<T> collection, IEqualityComparer<T> comparer) : base(
            collection, comparer)
        {
            this.filterType = filterType;
        }

        protected FilterHashSet(FilterType filterType, SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
            this.filterType = filterType;
        }

        public FilterHashSet(FilterType filterType, int capacity, IEqualityComparer<T> comparer) : base(capacity,
            comparer)
        {
            this.filterType = filterType;
        }

        public bool PassesFilter(T obj)
        {
            var contains = Contains(obj);
            switch (filterType)
            {
                case FilterType.BlackList:
                    return !contains;
                case FilterType.WhiteList:
                    return contains;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
