namespace TagsCloudVisualization
{
    public class FrequencyTags
    {
        private Dictionary<string, int> repeatDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> GetDictionaryWithTags(IEnumerable<string> splitStrings)
        {
            if (splitStrings == null)
                throw new ArgumentNullException();
            foreach (var splitString in splitStrings.Select(x=>x.ToLower()))
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