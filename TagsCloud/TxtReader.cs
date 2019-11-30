using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TxtReader: ITextReader
	{
		private readonly string fileName;

		public TxtReader(string fileName) => this.fileName = fileName;

		public string[] Read() => File.ReadAllLines(fileName);
	}
}