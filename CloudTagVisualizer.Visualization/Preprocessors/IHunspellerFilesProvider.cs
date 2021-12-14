namespace Visualization.Preprocessors
{
    public interface IHunspellerFilesProvider
    {
        public string GetDicFile();
        public string GetAffFile();
    }
}