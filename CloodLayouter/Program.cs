using System;
using System.Windows.Forms.VisualStyles;
using Autofac;
using CloodLayouter.App;
using CommandLine;

namespace CloodLayouter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
        var parserResult = CommandLine.Parser.Default.ParseArguments<Options>(args);
            var builder = new Bilder();
            var container = builder.Bild(parserResult);
            var perfomer = container.Resolve<ImagePerfomer>();
            perfomer.DrawAndSave();
        }
    }
}