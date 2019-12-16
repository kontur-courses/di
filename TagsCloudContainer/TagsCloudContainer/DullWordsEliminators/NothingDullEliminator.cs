namespace TagsCloudContainer
{
    public class NothingDullEliminator : IDullWordsEliminator
    {
        public NothingDullEliminator()
        { }

        public bool IsDull(string s)
        {
            return false;
        }
    }
}