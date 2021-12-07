namespace CloudTagContainer
{
    public interface IFileReader
    {
        public void SetPath(string path);
        public string[] ReadWords();
    }
}