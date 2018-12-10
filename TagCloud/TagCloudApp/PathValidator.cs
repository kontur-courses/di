using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloudApp
{
    internal class PathValidator : IPathValidator
    {
        private readonly HashSet<Type> nonValidExceptions = new HashSet<Type>
        {
            typeof(ArgumentException), typeof(PathTooLongException), typeof(NotSupportedException)
        };

        public bool Validate(string path)
        {
            try
            {
                Path.GetFullPath(path);
            }
            catch (Exception ex) when (nonValidExceptions.Contains(ex.GetType()))
            {
                return false;
            }

            return true;
        }
    }
}
