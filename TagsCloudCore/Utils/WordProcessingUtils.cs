namespace TagsCloudCore.Utils;

public static class WordProcessingUtils
{
    public static HashSet<string> RemoveDuplicates(IEnumerable<string> lines)
    {
        return lines
            .Distinct()
            .ToHashSet();
    }
}