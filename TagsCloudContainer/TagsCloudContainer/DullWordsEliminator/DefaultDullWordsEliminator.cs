namespace TagsCloudContainer
{
    public class DefaultDullWordsEliminator : DullWordEliminator
    {   
        public override bool IsDull(string s)
        {
            return tagger.tagString(s).Contains("IN") || tagger.tagString(s).Contains("DT") ||
                   tagger.tagString(s).Contains("CC") || tagger.tagString(s).Contains("PRP$") ||
                   tagger.tagString(s).Contains("PRP") || tagger.tagString(s).Contains("TO") ||
                   tagger.tagString(s).Contains("VBP") || tagger.tagString(s).Contains("VBZ") ||
                   tagger.tagString(s).Contains("LS") || s.Length == 1;
        }
    }
}