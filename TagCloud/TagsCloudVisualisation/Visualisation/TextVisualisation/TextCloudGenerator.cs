using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.Extensions;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public class TextCloudGenerator
    {
        private readonly ITextAnalyzer textAnalyzer;
        private readonly bool delayedDrawing;
        private readonly Font fontPrototype;
        private readonly Func<Brush> brushFactory;
        private readonly WordsLayouterAsset wordsLayouter;

        public TextCloudGenerator(ITextAnalyzer textAnalyzer, WordsLayouterAsset wordsLayouter, bool delayedDrawing,
            Font fontPrototype, Func<Brush> brushFactory)
        {
            this.textAnalyzer = textAnalyzer;
            this.wordsLayouter = wordsLayouter;
            this.delayedDrawing = delayedDrawing;
            this.fontPrototype = fontPrototype;
            this.brushFactory = brushFactory;
        }

        public void RegisterText(string text) => textAnalyzer.RegisterText(text);
        
        public void GenerateCloud(params int[] wordsSizeCoefficients) => GenerateCloud(-1, wordsSizeCoefficients);

        public void GenerateCloud(int maxWordsCount, params int[] wordsSizeCoefficients)
        {
            wordsSizeCoefficients = wordsSizeCoefficients.OrderByDescending(x => x).ToArray();
            var wordsEnumerable = textAnalyzer.GetSortedWords();
            var words = (maxWordsCount > 0
                    ? wordsEnumerable.Take(maxWordsCount)
                    : wordsEnumerable).ToArray();

            for (var i = 0; i < wordsSizeCoefficients.Length; i++)
            {
                if (!words.Any())
                    return;

                var elementsCount = Math.Min((int) Math.Pow(2, i), words.Length);
                words = words.TakeSkip(elementsCount, out var skipped).ToArray();
                DrawWords(CreateWords(skipped, wordsSizeCoefficients[i]));
            }

            DrawWords(CreateWords(words, wordsSizeCoefficients.Last()));
        }

        private void DrawWords(IEnumerable<WordToDraw> wordsToDraw)
        {
            if (delayedDrawing)
                foreach (var wordToDraw in wordsToDraw)
                    wordsLayouter.EnqueueWord(wordToDraw);
            else
                foreach (var wordToDraw in wordsToDraw)
                    wordsLayouter.DrawWord(wordToDraw);
        }

        private IEnumerable<WordToDraw> CreateWords(IEnumerable<string> words, int sizeCoefficient) => words.Select(w =>
            new WordToDraw(w, WordToDraw.MultiplyFontSize(fontPrototype, sizeCoefficient), brushFactory.Invoke()));
    }
}