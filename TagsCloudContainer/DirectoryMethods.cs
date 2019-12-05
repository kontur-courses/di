using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    static class DirectoryMethods
    {
        public static DirectoryInfo GetProjectDirectoryInfo()
        {
            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory);
            while (directoryInfo != null && directoryInfo.Name != "TagsCloudContainer")
                directoryInfo = directoryInfo.Parent;
            if (directoryInfo == null)
                throw new DirectoryNotFoundException("Directory with the name TagsCloudContainer not found");
            return directoryInfo;
        }
    }
}
