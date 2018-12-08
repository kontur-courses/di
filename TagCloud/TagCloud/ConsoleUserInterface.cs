using System;
using System.Collections.Generic;
using CommandLine;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class ConsoleUserInterface : UserInterface
    {
        public ConsoleUserInterface(TagCloudCreator creator, IEnumerable<ITextReader> readers) : base(creator, readers)
        {
        }

        public override void Run(string[] startupArgs)
        {
            Parser.Default.ParseArguments<Options>(startupArgs)
                  .WithParsed(o =>
                  {
                      var path = o.Path;
                      if (path == null)
                      {
                          path = ".";
                          Console.WriteLine("Using default path");
                      }

                      var options = new TagCloudOptions(null, new Point(), 3);
                      var image = Creator.CreateImage(options);
                      SaveImage(image, path);
                      Console.WriteLine("Image is saved!");
                  });
        }

        private void SaveImage(TagCloudImage image, string path)
        {
            throw new NotImplementedException();
        }

        public class Options
        {
            [Value(0, MetaName = "path", HelpText = "Path where to save image")]
            public string Path { get; set; }
        }
    }
}
