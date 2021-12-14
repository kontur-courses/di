namespace Visualization.Preprocessors
{
    public class RussianHunspellerFilesProvider: IHunspellerFilesProvider
    {
        public string GetDicFile()
        {
            return "Russian.dic";
        }

        public string GetAffFile()
        {
            return "Russian.aff";
        }
    }
}