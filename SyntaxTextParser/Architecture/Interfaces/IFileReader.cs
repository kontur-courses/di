namespace SyntaxTextParser.Architecture
{
    public interface IFileReader
    {
        bool TryReadText(string filePath, out string text);
        bool CanReadThatType(string type);
    }
}