using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TagsProcessor: ITagsProcessor
	{
		private readonly IWordsProcessor _wordsProcessor;
		private readonly FontSettings _settings;
		private readonly IImageHolder _imageHolder;

		public TagsProcessor(IWordsProcessor wordsProcessor, FontSettings settings, IImageHolder imageHolder)
		{
			_wordsProcessor = wordsProcessor;
			_settings = settings;
			_imageHolder = imageHolder;
		}

		public IEnumerable<Tag> GetTags() =>
			from word in _wordsProcessor.GetSortedWordsWithFrequencies() 
			let fontSize = CalculateFontSize(word) 
			let wordSize = GetWordSize(word.Text, fontSize) 
			select new Tag(word.Text, fontSize, new Rectangle(Point.Empty, wordSize));

		private Size GetWordSize(string word, int fontSize)
		{
			var graphics = _imageHolder.GetGraphics();
			var font = new Font(_settings.Font.Name, fontSize);
			var wordSize = graphics.MeasureString(word, font);
			wordSize = new SizeF(wordSize.Width * 1.04F, wordSize.Height * 0.8F);
			return wordSize.ToSize();
		}

		private int CalculateFontSize(Word word)
		{
			var size = Math.Log(word.Frequency, 1.09);
			size = size > _settings.MaxFontSize ? _settings.MaxFontSize : size;
			return (int) (size < _settings.MinFontSize ? _settings.MinFontSize : size);
		}
	}
}