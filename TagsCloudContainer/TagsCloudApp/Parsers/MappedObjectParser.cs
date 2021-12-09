using System;
using System.Collections.Generic;

namespace TagsCloudApp.Parsers
{
    public class MappedObjectParser<T>
    {
        private readonly Dictionary<string, T> map;

        public MappedObjectParser(Dictionary<string, T> map)
        {
            this.map = map;
        }

        public T Parse(string value)
        {
            if (map.TryGetValue(value, out var mappedObject))
                return mappedObject;

            throw new ApplicationException(
                $"Incorrect value: {value}\nAvailable values: {string.Join(", ", map.Keys)}");
        }
    }
}