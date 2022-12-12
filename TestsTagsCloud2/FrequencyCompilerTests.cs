using FluentAssertions;
using TagsCloud2.FrequencyCompiler;

namespace TestsTagsCloud;

public class FrequencyCompilerTests
{
    [Test]
    public void FrequencyCompiler_GetFrequencyOfWords()
    {
        var frequencyCompiler = new FrequencyCompiler();

        var words = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            words.Add("мама");
        }
        words.Add("чистила");
        words.Add("раму");

        var frequencyOfWords = frequencyCompiler.GetFrequencyOfWords(words, new HashSet<string>());

        frequencyOfWords["мама"].Should().Be(3);
        frequencyOfWords["чистила"].Should().Be(1);
        frequencyOfWords["раму"].Should().Be(1);
    }
    
    [Test]
    public void FrequencyCompiler_GetFrequencyList()
    {
        var frequencyCompiler = new FrequencyCompiler();

        var frequencyOfWords = new Dictionary<string, int>
        {
            { "мама", 3 },
            { "мыла", 2 },
            { "раму", 1 }
        };

        var frequencyList = frequencyCompiler.GetFrequencyList(frequencyOfWords, 3);
        frequencyList[0].Frequency.Should().Be(3);
        frequencyList[0].Word.Should().Be("мама");
        frequencyList[1].Frequency.Should().Be(2);
        frequencyList[1].Word.Should().Be("мыла");
        frequencyList[2].Frequency.Should().Be(1);
        frequencyList[2].Word.Should().Be("раму");
    }
}