using System.IO;

namespace TagsCloudBuilder
{
    public class TxtReader : Reader
    {
        public readonly string FileName;

        public TxtReader(string fileName)
        {
            FileName = fileName;
        }

        public string[] ReadAllLines()
        {
            try
            {
                return File.ReadAllLines(FileName);
            }
            catch (IOException e)
            {
                throw new IOException("Something went wrong. Check the correctness of file path.", e);
            }
        }

        public static string[] ReadAllLines(string fileName)
        {
            try
            {
                return File.ReadAllLines(fileName);
            }
            catch (IOException e)
            {
                throw new IOException("Something went wrong. Check the correctness of file path.", e);
            }
        }
    }
}
