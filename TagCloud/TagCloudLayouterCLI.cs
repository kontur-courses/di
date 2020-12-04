using System;
using System.IO;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;

namespace TagCloud
{
    public class TagCloudLayouterCLI : IApp
    {
        private readonly IReader<string> reader;
        private readonly WordAnalyzer<string> wordAnalyzer;
        private readonly Func<Settings> settingsFactory;
        private readonly IPainter<string> painter;

        public TagCloudLayouterCLI(IReader<string> reader, WordAnalyzer<string> wordAnalyzer, Func<Settings> settingsFactory, IPainter<string> painter)
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
            image.Save(imagePath);
            Console.WriteLine("Image saved");
            Console.WriteLine(Path.GetFullPath(imagePath));
        }
    }
}