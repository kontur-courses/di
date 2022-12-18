using System;
using System.IO;

namespace TagsCloudContainerTests
{
    public static class Utilities
    {
        public static string ProjectPath =>
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    }
}