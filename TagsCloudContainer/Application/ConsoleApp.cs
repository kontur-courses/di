using System;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Visualisators;

namespace TagsCloudContainer.Application
{
    public class ConsoleApp : IApp
    {
        private readonly IVisualisator _visualisator;
        
        public ConsoleApp(IVisualisator visualisator)
        {
            _visualisator = visualisator;
        }

        public void Run()
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _visualisator.Paint();
            _visualisator.Save(projectDirectory + "\\Results", "Rectangles", ImageFormat.Png);
        }
    }
}