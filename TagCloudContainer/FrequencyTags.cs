namespace TagsCloudVisualization
{
    internal class FrequencyTags
    {
        private IDictionary<string, int> repeatDictionary = new Dictionary<string, int>();
        public IDictionary<string, int> GetDictionaryWithTags(IEnumerable<string> splitStrings)
        {
            if (splitStrings == null)
                throw new ArgumentNullException();
            foreach (var splitString in splitStrings)
            {
                if (!repeatDictionary.ContainsKey(splitString))
                    repeatDictionary[splitString] = 0;
                repeatDictionary[splitString]++;
            }
            repeatDictionary = repeatDictionary.OrderByDescending(order => order.Value)
                .ToDictionary(x => x.Key, y => y.Value);
            return repeatDictionary;
        }
    }
}