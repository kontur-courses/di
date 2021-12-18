using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public class BoringWords : IExcludingWords
    {
        private readonly HashSet<string> Words;

        public BoringWords()
        {
            Words = new HashSet<string>
            {
                //modal verbs
                "can", "can't", "could", "couldn't", "may", "might", "must", "need",
                "ought", "should", "shouldn't", "would", "wouldn't", "shall", 
                "will", "won't", "dare",
                //pronouns
                "i", "you", "he", "she", "who", "it", "we", "they",
                "me", "whom", "him", "her", "us", "them",
                "my", "mine", "his", "hers", "your", "yours", "our",
                "ours", "their", "theirs", "whose", "its",
                //articles
                "a", "an", "the",
                //other auxilliary verbs
                "do", "don't", "does", "doesn't", "did", "didn't", "am",
                "not", "does", "able", "was", "wasn't", "were", "weren't",
                "be", "being", "have", "haven't", "has", "hasn't",  "is",
                "isn't", "are", "aren't",
                //the most common prepositions
                "of", "in", "to", "for", "with", "on", "at", "from", "by",
                "about", "as", "into", "like", "through", "after", "over",
                "between", "out", "against", "during", "withot", "before",
                "under", "around", "among",
                //other
                "there", "that", "this", "those", "these", "which", "where",
                "some", "had", "why", "but", "and", "most", "when", "been",
                "all", "such", "since", "yes", "no", "or", "too", "many"
            };
        }

        public HashSet<string> GetWords()
            => new HashSet<string>(Words);
    }
}
