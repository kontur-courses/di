using System;
using System.Collections;
using System.Collections.Generic;

namespace TagsCloudContainerTests;

public class StringSpaceSplitter : IEnumerable<string>
{
    private List<string> Words;

    public StringSpaceSplitter(string testString)
    {
        Words = new List<string>(testString.Split());
        Words.RemoveAll(s => s.Trim() == String.Empty);
    }

    public IEnumerator<string> GetEnumerator() => Words.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}