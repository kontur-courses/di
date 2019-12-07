using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    internal class FrequencySelector : IFrequencyObjectSelector<string>
    {
        private static readonly string BadWords =
            " the to and a he of was harry in his it said had you at on they that as him i with ron but for be " +
            "out all up them hagrid were have what back hermione from one there she if into their about been this " +
            "not didn�t off so could got get like when down her looked  over very know me just professor who see by is then your around do  are no going an snape through dumbledore" +
            " uncle think now we never time more  something  dudley looking first how go eyes door ";

        private readonly HashSet<string> BadSet = BadWords.Split(" ").ToHashSet();

        public bool Select(string obj)
        {
            return !BadSet.Contains(obj);
        }
    }
}