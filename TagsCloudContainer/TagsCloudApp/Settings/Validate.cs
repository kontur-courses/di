using System;

namespace TagsCloudApp.Settings
{
    public static class Validate
    {
        public static T Positive<T>(string valueName, T value) where T : IComparable<T>
        {
            if (value.CompareTo(default) <= 0)
                throw new ApplicationException($"{valueName} must be positive.");

            return value;
        }
    }
}