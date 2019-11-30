namespace TagsCloud
{
	public class Word
	{
		public string Text { get; }
		public int Frequency { get; }

		public Word(string text, int frequency)
		{
			Text = text;
			Frequency = frequency;
		}
	}
}