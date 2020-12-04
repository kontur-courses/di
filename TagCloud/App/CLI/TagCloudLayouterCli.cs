using System;
using System.IO;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;

namespace TagCloud.App.CLI
{
    public class TagCloudLayouterCli : IApp
    {
        private readonly IPainter<string> painter;
        private readonly IReader<string> reader;
        private readonly Func<Settings> settingsFactory;
        private readonly WordAnalyzer<string> wordAnalyzer;

        public TagCloudLayouterCli(IReader<string> reader, WordAnalyzer<string> wordAnalyzer,
            Func<Settings> settingsFactory, IPainter<string> painter)
        {
            this.reader = reader;
            this.wordAnalyzer = wordAnalyzer;
            this.settingsFactory = settingsFactory;
            this.painter = painter;
        }

        public void Run()
        {
            settingsFactory().Import(Program.GetDefaultSettings());
            Console.WriteLine("Default settings imported");
            var tokens = reader.ReadTokens();
            var analyzedTokens = wordAnalyzer.Analyze(tokens);
            Console.WriteLine("Tokens analyzed");
            using var image = painter.GetImage(analyzedTokens);
            Console.WriteLine("Layout ready");
            var imagePath = settingsFactory().ImagePath;
            Console.WriteLine($"Saving into {Path.GetFullPath(imagePath)}");
            image.Save(imagePath);
            Console.WriteLine("Image saved");
        }
    }
}