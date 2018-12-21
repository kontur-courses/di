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
            var builder = new DIBilder();
            var container = builder.Bild(parserResult);
            var saver = container.Resolve<IImageSaver>();
            parserResult.WithParsed(opt => saver.Save(opt.OutputFile));
        }
    }
}