using System.Drawing.Imaging;
using Autofac;
using CommandLine;
using TagsCloudVisualization.ApplicationOptions;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var parsedArguments = Parser.Default.ParseArguments<ApplicationOptions.ApplicationOptions>(args);
            var applicationOptions = new ApplicationOptionsExtractor().GetOption(parsedArguments);
            var container = new ContainerCreator().GetContainer(applicationOptions);
            var cloudCreator = container.Resolve<CloudCreator>();
            var cloud = cloudCreator.GetCloud(applicationOptions.TextName);
            ImageSaver.SaveImage(applicationOptions.ImagePath, cloud, ImageFormat.Png);
        }
    }
}