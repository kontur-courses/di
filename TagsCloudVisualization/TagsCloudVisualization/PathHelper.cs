using System;
using System.IO;

namespace TagsCloudVisualization
{
    internal class PathHelper
    {
        public static string ResourcesPath
        {
            get
            {
                var pathToResources = Environment.CurrentDirectory;

                for (var i = 0; i < 3; i++)
                {
                    pathToResources = Directory.GetParent(pathToResources).FullName;
                }

                pathToResources += "\\Resources";
                return pathToResources;
            }
        }
    }
}