using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Core.Drawers;
using TagsCloudVisualization.Core.Translators;

namespace TagsCloudVisualization
{
    public class Program
    {
        static void Main(string[] args)
        {
//            var builder = new ContainerBuilder();
//            builder.RegisterType<ArchimedeanSpiral>().As<ISpiral>();
//            builder.RegisterType<SpiralRectangleCloudLayouter>().As<IRectangleCloudLayouter>();
//            builder.RegisterType<RectangleCloudDrawer>().As<IRectangleCloudDrawer>();
//            var container = builder.Build();
//            
//            UsageExamples.GenerateFirstTagCloud();
//            UsageExamples.GenerateSecondTagCloud();
//            UsageExamples.GenerateThirdTagCloud();
//            UsageExamples.GenerateFourthTagCloud();

            var tags = new TextToTagsTranslator().TranslateTextToTags(
                File.ReadLines("exampleText.txt"), new HashSet<string>());
            
            var cloudDrawer = new RectangleCloudDrawer(Color.Teal, Brushes.Peru);
            cloudDrawer.DrawCloud(
                tags.ToList(),
                Environment.CurrentDirectory + $"\\test.png");
        }
    }
}