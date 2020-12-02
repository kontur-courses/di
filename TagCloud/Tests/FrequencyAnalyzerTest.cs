using System;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.Tests
{
    public class FrequencyAnalyzerTest
    {
        private FrequencyAnalyzer.FrequencyAnalyzer frequencyAnalyzer = new FrequencyAnalyzer.FrequencyAnalyzer(new TagCloud.LiteratureTextParser(new PathCreater()));
        
        [Test]
        public void FrequencyAnalyzerReturnDictionaryWithCorrectFrequencies()
        {
            var dict = frequencyAnalyzer.GetFrequencyDictionary("input.txt");
            var wordsCount = 24;
            dict.Should().HaveCount(13);
            dict["кошка"].Should().BeInRange((double) 6 / wordsCount - Double.Epsilon, (double) 6 / wordsCount + Double.Epsilon);
            dict["собака"].Should().BeInRange((double) 2 / wordsCount - Double.Epsilon, (double) 2 / wordsCount + Double.Epsilon);
            dict["крокодил"].Should().BeInRange((double) 1 / wordsCount - Double.Epsilon, (double) 1 / wordsCount + Double.Epsilon);
        }
    }
}