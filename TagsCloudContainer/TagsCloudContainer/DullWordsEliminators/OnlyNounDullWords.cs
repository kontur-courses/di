namespace TagsCloudContainer
{
    public class OnlyNounDullWordsEliminator : DullWordEliminator
    {
        public OnlyNounDullWordsEliminator() : base()
        { }

        public override bool IsDull(string s)
        {
            return !(tagger.tagString(s).Contains("NN") || tagger.tagString(s).Contains("NNS") ||
                     tagger.tagString(s).Contains("NNP") || tagger.tagString(s).Contains("NNPS"));
        }
    }
}