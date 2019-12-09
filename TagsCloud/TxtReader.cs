using System;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TxtReader: ITextReader
	{
		private readonly string fileName;

		public TxtReader(string fileName) => this.fileName = fileName;

		public string[] Read()
		{
			var lines = File.ReadAllLines(fileName);
			if (lines.Length == 0)
				throw new Exception("File is empty");
			return lines;
		}
	}
}