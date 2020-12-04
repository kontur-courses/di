using System;
using System.IO;
using Gtk;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Text;
using Settings = TagCloud.Infrastructure.Settings.Settings;

namespace TagCloud.App.GUI
{
    internal class TagCloudLayouterGui : IApp
    {
        private readonly IPainter<string> painter;


        private readonly IReader<string> reader;
        private readonly Func<Settings> settingsFactory;
        private readonly WordAnalyzer<string> wordAnalyzer;

        public TagCloudLayouterGui(IReader<string> reader, WordAnalyzer<string> wordAnalyzer,
            Func<Settings> settingsFactory, IPainter<string> painter)
        {
            this.reader = reader;
            this.wordAnalyzer = wordAnalyzer;
            this.settingsFactory = settingsFactory;
            this.painter = painter;

            Application.Init();
            var window = new Window("Tag Cloud Layouter");
            var button = new Button("Write to console");
            window.DeleteEvent += Close;
            button.Clicked += Click;
            window.Add(button);
            window.ShowAll();
        }

        public void Run()
        {
            settingsFactory().Import(Program.GetDefaultSettings());
            Console.WriteLine("Default settings imported");

            Application.Run();
        }

        private void Click(object obj, EventArgs e)
        {
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

        private void Close(object obj, DeleteEventArgs e)
        {
            Console.WriteLine("Closeed!");
            Application.Quit();
        }
    }
}