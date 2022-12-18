using System.Collections;
using System.Collections.Generic;
using System.IO;
using TagsCloudContainer;

namespace TagsCloudContainerTests;

public class TestFileReader : IWordSequenceProvider, IWordFilterProvider
{
    public Result<IEnumerable<string>> WordSequence => new(wordSeq);
    private List<string> wordSeq;
    public Result<IEnumerable<string>> WordFilter => new(wordFilt);
    private List<string> wordFilt;

    public TestFileReader(string wordsSeqPath, string wordsFiltPath)
    {
        wordSeq = new List<string>();
        foreach (var line in File.ReadAllLines(wordsSeqPath)) wordSeq.AddRange(line.Split());
        
        wordFilt = new List<string>();
        if(wordsFiltPath!=null)
            foreach (var line in File.ReadAllLines(wordsFiltPath)) wordFilt.AddRange(line.Split());
    }
}