using System;
using System.Drawing.Imaging;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Input;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer
{
    public class Transformer
    {
        private readonly IFileReader fileReader;
        private readonly WordParser parser;
        private readonly WordLayout layout;
        private readonly IDrawer drawer;
        private readonly IWriter writer;

        private readonly ImageSettings settings;

        public Transformer(IFileReader fileReader, IWriter writer, IDrawer drawer, WordParser parser, WordLayout layout, ImageSettings settings)
        {
            this.fileReader = fileReader;
            this.writer = writer;
            this.drawer = drawer;
            this.parser = parser;
            this.layout = layout;
            this.settings = settings;
        }

        public void TransformWords(string textFile, string imageFile)
        {
            Console.WriteLine($"Reading from: {textFile}");
            Console.WriteLine($"Writing to: {imageFile}");

            var text = fileReader.Read(textFile);
            var parsedWords = parser.ParseWords(text);

            layout.PlaceWords(parsedWords);
            var bitmap = drawer.Draw(layout, settings, out var graphics);
            // ??? я так и не понял как пробрасывать graphics в layout

            writer.WriteToFile(bitmap, imageFile, ImageFormat.Png);
        }
    }
}