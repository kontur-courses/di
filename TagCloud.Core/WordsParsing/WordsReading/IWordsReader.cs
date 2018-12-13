using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagCloud.Core.WordsParsing.WordsReading
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadFrom(string path);

        /// <summary>
        /// <returns>
        /// Regular expression represents extension of files allowed by this reader (including dot)
        /// </returns>
        /// </summary>
        /// <remarks>
        /// Dot is needed here to differ ".abc_x" and ".x"
        /// </remarks>
        Regex AllowedFileExtension { get; }
    }
}