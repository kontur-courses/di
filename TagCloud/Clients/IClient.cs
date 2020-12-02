using TagCloud.Visualization;

namespace TagCloud.Clients
{
    internal interface IClient
    {
        public void Run();

        public void Visualization(string wordsPath, string picturePath, VisualizationInfo info);
    }
}
