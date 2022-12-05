using System.Collections;
using System.Collections.Generic;
using System.IO;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

public class TestFileReader
{
    public List<string> Words;
    public List<string> ExcludedWords;

    public TestFileReader(ISettingsProvider settingsProvider)
    {
        Words = new List<string>();
        foreach (var line in File.ReadAllLines(settingsProvider.Settings.InputPath)) Words.AddRange(line.Split());
        
        ExcludedWords = new List<string>();
        if(settingsProvider.Settings.FilterPath!=null)
            foreach (var line in File.ReadAllLines(settingsProvider.Settings.FilterPath)) ExcludedWords.AddRange(line.Split());
    }
}