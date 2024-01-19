namespace TagsCloudContainer.Utils;

public static class WordProcessingUtils
{
    public static HashSet<string> RemoveDuplicates(IEnumerable<string> lines)
        => lines
            .Distinct()
            .ToHashSet();
}