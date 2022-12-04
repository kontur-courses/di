using System.Collections;
using System.Collections.Generic;
using System.IO;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

public class TestFileReader : IEnumerable<string>
{
    public List<string> Words;

    public TestFileReader(ISettingsProvider settingsProvider)
    {
        Words = new List<string>();
        foreach (var line in File.ReadAllLines(settingsProvider.Settings.InputPath)) Words.AddRange(line.Split());
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