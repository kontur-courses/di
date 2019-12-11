using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TagsProcessor: ITagsProcessor
	{
		private const float WordWidthScale = 1.04F;
		private const float WordHeightScale = 0.8F;
		private const double FontSizeScale = 1.09;
		private readonly IWordsProcessor wordsProcessor;
		private readonly FontSettings settings;
		private readonly IImageHolder imageHolder;

		public TagsProcessor(IWordsProcessor wordsProcessor, FontSettings settings, IImageHolder imageHolder)
		{
			this.wordsProcessor = wordsProcessor;
			this.settings = settings;
			this.imageHolder = imageHolder;
		}

		public IEnumerable<Tag> GetTags() =>
			from word in wordsProcessor.GetWordsWithFrequencies()
			let fontSize = CalculateFontSize(word)
			let wordSize = GetWordSize(word.Text, fontSize) 
			select new Tag(word.Text, fontSize, new Rectangle(Point.Empty, wordSize));

		private Size GetWordSize(string word, int fontSize)
		{
			var graphics = imageHolder.GetGraphics();
			var font = new Font(settings.Font.Name, fontSize);
			var wordSize = graphics.MeasureString(word, font);
			wordSize = new SizeF(wordSize.Width * WordWidthScale, wordSize.Height * WordHeightScale);
			return wordSize.ToSize();
		}

		internal int CalculateFontSize(Word word)
		{
			if (word.Frequency < 1)
				throw new ArgumentException("Word frequency must be grater or equal 1");
			if (settings.MaxFontSize < 1 || settings.MaxFontSize < settings.MinFontSize)
				throw new ArgumentException("MaxFontSize must be grater than 1 and grater or equal minFontSize");
			if (settings.MinFontSize < 1)
				throw new ArgumentException("MinFontSize must be grater than 1");
			
			var size = Math.Log(word.Frequency, FontSizeScale);
			size = size > settings.MaxFontSize ? settings.MaxFontSize : size;
			return (int) (size < settings.MinFontSize ? settings.MinFontSize : size);
		}
	}
}