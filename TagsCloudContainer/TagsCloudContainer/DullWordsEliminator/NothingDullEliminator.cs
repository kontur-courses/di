namespace TagsCloudContainer
{
    public class NothingDullEliminator : IDullWordsEliminator
    {
        public bool IsDull(string s)
        {
            return false;
        }
    }
}

