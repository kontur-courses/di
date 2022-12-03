using System.Collections;
using System.Collections.Generic;

namespace TagsCloudContainerTests;

public class StringSpaceSplitter : IEnumerable<string>
{
    private readonly List<string> Words;

    public StringSpaceSplitter(string testString)
    {
        Words = new List<string>(testString.Split());
        Words.RemoveAll(s => s.Trim() == string.Empty);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return Words.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}