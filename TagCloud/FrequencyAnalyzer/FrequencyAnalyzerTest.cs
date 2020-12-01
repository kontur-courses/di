using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.FrequencyAnalyzer
{
    public class FrequencyAnalyzerTest
    {
        private FrequencyAnalyzer frequencyAnalyzer = new FrequencyAnalyzer(new LiteratureTextParser(new PathCreater()));
        
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