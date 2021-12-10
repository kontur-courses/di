using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CommandLine;
using TagCloud.Templates.Colors;

namespace TagCloud.Configurations
{
    public class CommandLineConfigurationProvider : IConfigurationProvider
    {
        private readonly IEnumerable<string> args;

        public CommandLineConfigurationProvider(IEnumerable<string> args)
        {
            this.args = args;
        }

        public Configuration GetConfiguration()
        {
            Options arguments;
            try
            {
                arguments = Parser.Default.ParseArguments<Options>(args).Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message);
                return null;
            }

            if (!File.Exists(arguments.Filename))
            {
                Console.WriteLine($"File {arguments.Filename} does not exist!");
                return null;
            }

            var configuration = new Configuration(arguments.Filename, arguments.Output);
            configuration.ImageSize = new Size(arguments.Width, arguments.Height);
            configuration.BackgroundColor = arguments.BackgroundColor;
            if (arguments.Color != Color.Empty)
                configuration.ColorGenerator = new OneColorGenerator(arguments.Color);
            if (arguments.FontFamily != null)
                configuration.FontFamily = arguments.FontFamily;
            return configuration;
        }
    }

    public interface IConfigurationProvider
    {
        Configuration GetConfiguration();
    }
}