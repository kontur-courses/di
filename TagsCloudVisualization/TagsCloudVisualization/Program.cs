using System;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class CloudWordsForm
    {
        private static void Main(string[] args)
        {
            var cloudParametersParser = new CloudParametersParser();
            var parameters = cloudParametersParser.Parse(args);

            if (!parameters.IsCorrect())
                return;

            var cloud = new CircularCloudLayouter(parameters.Curve);
            var wordDataHandler = new WordDataHandler();
            var data = wordDataHandler.GetDatas(cloud);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }
    }
}