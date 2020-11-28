using System.IO;

namespace TagCloud
{
    internal class TextReader
    {
        internal string ReadText(string path)
        {
            if (TryReadFile(path, out var result))
                return result;
            return result;
        }
        private bool TryReadFile(string path, out string result)
        {
            using (var sr = File.OpenText(path))
            {
                try
                {
                    result = sr.ReadToEnd();
                }
                catch
                {
                    result = null;
                }
            }
            return result != null;
        }
    }
}
