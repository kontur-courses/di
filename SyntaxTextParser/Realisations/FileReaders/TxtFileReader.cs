using System.IO;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class TxtFileReader : IFileReader
    {
        public bool TryReadText(string filePath, out string text)
        {
            text = File.ReadAllText(filePath);
            return true;
        }

        public bool CanReadThatType(string type)
        {
            return type == "txt";
        }
    }
}