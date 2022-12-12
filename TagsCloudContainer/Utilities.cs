using System;
using System.IO;

namespace TagsCloudContainer
{
    public static class Utilities
    {
        public static string GetProjectPath()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            return directoryInfo.Parent.Parent.Parent.FullName;
        }

        public static int CalcFontSize(int minFontSize, int maxFontSize,
            int weight, int minWeight, int maxWeight)
        {
            var fontSize = maxFontSize * (weight - minWeight) / (maxWeight - minWeight);
            return Math.Max(fontSize, minFontSize);
        }
    }
}