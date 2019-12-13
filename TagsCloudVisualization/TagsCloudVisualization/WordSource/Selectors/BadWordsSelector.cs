using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Selectors
{
    internal class BadWordsSelector : ISelector<string>
    {
        private static string standartBadWords =
            " the to and a he of was harry in his it said had you at on they that as him i with ron but for be " +
            "out all up them hagrid were have what back hermione from one there she if into their about been this " +
            "not didn't off so could got get like when down her looked  over very know me just professor who see by is then your around do  are no going an snape through dumbledore" +
            " uncle think now we never time more  something  dudley looking first how go eyes door ";


        private readonly HashSet<string> badSet;

        public BadWordsSelector(string badWords)
        {
            //Не представляю как заменить на полиморфизм
            if (badWords.Length == 0)
                badWords = standartBadWords;
            badSet = badWords.Split(" ").ToHashSet();
        }

        public bool Select(string obj)
        {
            return !badSet.Contains(obj);
        }
    }
}