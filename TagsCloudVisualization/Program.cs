using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using TagsCloudVisualization.Core;
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
            
            // ToDo нарисовать теги
        }
    }
}