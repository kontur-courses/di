using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    public class FrequencyDictionaryTests
    {
        [Test]
        public void GetNormalizedFreqDic()
        {
            var frequencyDictionary = new FrequencyCollection();
            var words = new List<string>
            {
                "car",
                "car",
                "auto",
                "auto",
                "auto",
                "automobile"
            };

            var actualDictionary = frequencyDictionary.GetFrequencyCollection(words)
                .ToDictionary(word => word.Key, word => word.Value);
            actualDictionary["car"].Should().BeInRange(32, 34);
            actualDictionary["auto"].Should().BeInRange(49, 51);
            actualDictionary["automobile"].Should().BeInRange(15, 17);
        }
    }
}