using System.Collections.Generic;
using Autofac;
using CloodLayouter.App;
using CloodLayouter.Infrastructer;
using CommandLine;

namespace CloodLayouter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<Options>(args);
            var builder = new Bilder();
            var container = builder.Bild(parserResult);
            var perfomer = container.Resolve<IImageSaver>();
            perfomer.Save();
        }
    }
}