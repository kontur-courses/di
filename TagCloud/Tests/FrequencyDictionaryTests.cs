using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    public class FrequencyDictionaryTests
    {
        [Test]
        public void GetNormalizedFreqDic()
        {
            var frequencyDictionary = new FrequencyDictionary();
            var words = new List<string>
            {
                "car",
                "car",
                "auto",
                "auto",
                "auto",
                "automobile"
            };

            var actualDictionary = frequencyDictionary.GetFrequencyDictionary(words);
            actualDictionary["car"].Should().BeInRange(32, 34);
            actualDictionary["auto"].Should().BeInRange(49, 51);
            actualDictionary["automobile"].Should().BeInRange(15, 17);
        }
    }
}