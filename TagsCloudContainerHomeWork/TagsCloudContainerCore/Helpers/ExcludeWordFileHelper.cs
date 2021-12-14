using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainerCore.Helpers;

public static class ExcludeWordFileHelper
{
    private static readonly HashSet<string> ExcludeWords  = new();

    public static void LoadWordsFromFile(string path)
    {
        if (path == null || !File.Exists(path))
        {
            return;
        }

        using var reader = new StreamReader(path);
        var nextLine = reader.ReadLine();
        while (nextLine is not null)
        {
            ExcludeWords.UnionWith(nextLine.Split(' ').Select(w=>w.ToLowerInvariant()));
            nextLine = reader.ReadLine();
        }
    }

    public static bool IsExclude(string word) 
        => ExcludeWords.Contains(word.ToLowerInvariant());
}