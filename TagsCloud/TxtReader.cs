using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TxtReader: ITextReader
	{
		private readonly string _fileName;

		public TxtReader(string fileName) => _fileName = fileName;

		public string[] Read() => File.ReadAllLines(_fileName);
	}
}