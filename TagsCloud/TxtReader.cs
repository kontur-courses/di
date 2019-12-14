using System;
using System.Data;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class TxtReader: ITextReader
	{
		private readonly string fileName;
		private readonly IExceptionHandler exceptionHandler;

		public TxtReader(string fileName, IExceptionHandler exceptionHandler)
		{
			this.fileName = fileName;
			this.exceptionHandler = exceptionHandler;
		}

		public string[] Read()
		{
			try
			{
				var lines = File.ReadAllLines(fileName);
				if (lines.Length == 0)
					throw new FileReadingException("File is empty");
				return lines;
			}
			catch (Exception e)
			{
				exceptionHandler.Handle(e);
				return new string[0];
			}
		}
	}
}