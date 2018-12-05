using System;
using CommandLine;
using TagsCloudVisualization;

namespace TagCloud
{
    class ConsoleUserInterface : UserInterface
    {
        public class Options
        {
            [Value(0, MetaName = "path", HelpText = "Path where to save image")]
            public string Path { get; set; }
        }

        public ConsoleUserInterface(TagCloudCreator creator, TagCloudOptions.Factory optionsFactory) : base(creator, optionsFactory)
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

                      var options = this.OptionsFactory.Invoke(null, new Point());
                      var image = this.Creator.CreateImage(options);
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