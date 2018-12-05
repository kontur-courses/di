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
    public class Program
    {
        private const string USAGE = @"TagsCloudVisualization.
        
        Usage: 
            TagsCloudVisualization.exe <file> [-o=<file>] [--ff=<fontfamily>] [--fs=<fontstyle>] [-p=<x,y>] [-s=<step,angle>] [--bg=<background>] [--fg=<foreground>] [--size=<width,height>] [-m=<size>]
            TagsCloudVisualization.exe (-h | --help)
            TagsCloudVisualization.exe (--version)
        
        Options:
            -h --help                Show this
            --version                Show version
            -o=<file>                Output file [default: ./examples/result.png]
            --ff=<fontfamily>        Font family [default: Times New Roman]
            --fs=<fontstyle>         Font style enum number [default: 0]
            -p=<x,y>                 Central point [default: 0,0]
            -s=<step,angle>          Algorithm spiral [default: 0.0005,0]
            --bg=<background>        Background color [default: DimGray]
            --fg=<foreground>        Foreground color [default: FloralWhite]
            --size=<width,height>    Size [default: 800,800]
            -m=<size>                Max font size [default: 100]";
        
        public static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(USAGE, args);
            var outfile = arguments["-o"].Value.ToString();
            var container = BuildContainer(arguments);
            DrawWordCloud(container, outfile);
        }

        private static IContainer BuildContainer(IDictionary<string, ValueObject> args)
        {
            var builder = new ContainerBuilder();
            var boringWords = GetBoringWords();
            builder.RegisterInstance(new TxtReader(args["<file>"].Value.ToString())).As<IFileReader>();
            builder.RegisterInstance(new FontFamily(args["--ff"].Value.ToString())).As<FontFamily>();
            builder.RegisterInstance(new TextPreprocessor(boringWords)).AsSelf();
            builder.RegisterInstance(args["-s"].Value.ToString().Split(',').ToSpiral()).As<Spiral>();
            builder.RegisterInstance(new SolidBrush(Color.FromName(args["--fg"].Value.ToString()))).As<Brush>();
            builder.Register(c => Color.FromName(args["--bg"].Value.ToString())).As<Color>();
            builder.Register(c => float.Parse(args["-m"].Value.ToString())).As<float>();
            builder.Register(c => (FontStyle)Enum.Parse(typeof(FontStyle), args["--fs"].Value.ToString(), true)).As<FontStyle>();
            builder.Register(c => new Size(args["--size"].Value.ToString().Split(',').ToPoint())).As<Size>();
            builder.Register(c => args["-p"].Value.ToString().Split(',').ToPoint()).As<Point>();
            builder.RegisterType<WordsCloudLayouter>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<FontSettings>().AsSelf();
            builder.RegisterType<LayouterSettings>().AsSelf();
            builder.RegisterType<WordsCloudVisualizer>().As<IVisualizer<Word>>();
            builder.RegisterType<Palette>().AsSelf();
            return builder.Build();
        }

        private static void DrawWordCloud(IContainer container, string outfile)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var textReader = scope.Resolve<IFileReader>();
                var frequencyWords = scope.Resolve<TextPreprocessor>().PreprocessWords(textReader.ReadToEnd()).ToList();
                var visualizer = scope.Resolve<IVisualizer<Word>>();
                var wordsLayouter = scope.Resolve<WordsCloudLayouter>();
                var words = wordsLayouter.LayWords(frequencyWords).ToList();
                visualizer.Draw(words).Save(outfile);
            }
        }

        private static string[] GetBoringWords()
        {
            using (var streamReader = new StreamReader("boring_words.txt", Encoding.UTF8))
                return streamReader.ReadToEnd().Split(new []{Environment.NewLine}, StringSplitOptions.None);
        }
    }
}
