using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Autofac;
using DocoptNet;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = DependenciesBuilder.BuildContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var textPreprocessor = scope.Resolve<TextPreprocessor>();
                var sizer = scope.Resolve<ISizer>();
                var layOuter = scope.Resolve<IWordsCloudLayouter>();
                var words = layOuter.LayWords(sizer.SizeWords(textPreprocessor)).ToList();
                var visualize = scope.Resolve<IVisualizer<IList<Word>>>();
                visualize.Draw(words).Save("examples/text.png");
            }
        }
    }
}
