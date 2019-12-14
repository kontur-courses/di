using System;

namespace TagsCloud
{
	public class FileReadingException: Exception
	{
		public FileReadingException(string message): base(message)
		{
		}
	}
}