using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloudContainer.ConfigCreators;
using TagsCloudVisualization;

namespace CloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Users\\ivan0\\Desktop\\di\\CloudContainer\\bin\\Debug\\netcoreapp3.1\\text.txt"; //TODO
            var boringWords = new List<string>{"в","под","на"};//TODO
            
            var cleaner = new BoringWordsCleaner(boringWords.ToHashSet());
            var provider = new TxtWordProvider(cleaner);
            var words = provider.GetWords(path);
            var configCreator = new ConsoleConfigCreator();
            var config = configCreator.CreateConfig(args);
            var pointProvider = new PointProvider(config);
            var cloudLayouter = new CircularCloudLayouter(pointProvider);
            var converter = new WordsToRectanglesConverter(cloudLayouter, config);
            var cloudTags = converter.ConvertWords(words);
            var image = Drawer.DrawImage(cloudTags,config);
            var imageSaver = new PngSaver();
            imageSaver.SaveImage(image, "newfile");
        }
    }
}