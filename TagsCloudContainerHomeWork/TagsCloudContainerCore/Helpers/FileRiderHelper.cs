using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainerCore.Helpers;

public static class FileRiderHelper
{
    public static IEnumerable<string> GetTags(string path)
    {
        var result = new List<string>();
        using var fileRider = new StreamReader(path);
        var tmp = fileRider.ReadLine();

        while (tmp is not null)
        {
            result.Add(tmp);
            tmp = fileRider.ReadLine();
        }

        return result;
    }
}