using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    public static class TagsLayouterHelper
    {
        public static string[] ReadTagsFromFile(string path)
        {
            var text = File.ReadAllText("test.txt");
            return text.Split("\r\n").Distinct().ToArray();
        }
    }
}
