using System;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TxtReader: ITextReader
	{
		private readonly string _fileName;

		public TxtReader(string fileName) => _fileName = fileName;

		public string[] Read()
		{
			try
			{
				var lines = File.ReadAllLines(_fileName);
				return lines;
			}
			catch (FileNotFoundException)
			{
				return new[] {$"File {_fileName} is not found"};
			}
			catch (FileLoadException)
			{
				return new[] {"Something went wrong while loading source text file"};
			}
		}
	}
}