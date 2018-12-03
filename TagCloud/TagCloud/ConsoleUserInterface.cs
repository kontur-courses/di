using System;
using CommandLine;

namespace TagCloud
{
    class ConsoleUserInterface : UserInterface
    {
        public class Options
        {
            [Value(0, MetaName = "path", HelpText = "Path where to save image")]
            public string Path { get; set; }
        }
        public ConsoleUserInterface(TagCloudCreator creator) : base(creator)
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
                      var image = Creator.CreateImage();
                      SaveImage(image, path);
                      Console.WriteLine("Image is saved!");
                  });
        }

        private void SaveImage(TagCloudImage image, string path)
        {
            throw new NotImplementedException();
        }
    }
}