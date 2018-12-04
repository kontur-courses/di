using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class TagsCloud
    {
        private IFileReader fileReader;
        private IEnumerable<string> words;
        private List<string> stopWords;
        private CircularCloudLayouter layouter;
        private Color wordsColor;

        public TagsCloud(IFileReader fileReader, CircularCloudLayouter layouter)
        {
            this.fileReader = fileReader;
            this.layouter = layouter;
            words = new List<string>();
            stopWords = new List<string>();
            wordsColor = Color.Black;
        }

        public void AddWordsFromFile(string sourcePath)
        {
            words = fileReader.ReadLines(sourcePath);
        }

        public void AddStopWords(string file)
        {
            stopWords.AddRange(fileReader.ReadLines(file));
        }

        public void SetColor(Color color)
        {
            wordsColor = color;
        }

        public void Compile(string path)
        {
            words = words.Select(s => s.ToLower());
            var tags = ConvertToTags(words);
            var compiled = new Dictionary<string, Rectangle>();
            foreach (var word in tags)
            {
                var size = new Size(word.Value.Length * word.Frequency * 25,  word.Frequency * 25);
                compiled[word.Value] = layouter.PutNextRectangle(size);
            }

            var cloud = layouter.GetCloud();
            var height = (int)cloud.GetHeight();
            var width = (int)cloud.GetWidth();
            Visualizator.ObjectsColor = wordsColor;
            compiled.VizualizeToFile(width, height, path);
        }

        private IEnumerable<Tag> ConvertToTags(IEnumerable<string> wordsEnumerable)
        {
            var result = new Dictionary<string, int>();
            foreach (var word in wordsEnumerable)
            {
                if (!result.ContainsKey(word))
                    result.Add(word, 0);
                result[word]++;
            }

            return result.Select(p => new Tag(p.Key, p.Value));
        }
    }
}