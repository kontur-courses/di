using System.Text;

namespace TagCloudGenerator
{
    public class TextProcessor : ITextProcessor
    {
        public string[] ProcessTheText(string[] file)
        {
            var result = new StringBuilder();

            for (var i = 0; i < file.Length; i++)
            {             
                file[i] = file[i].ToLower();
            }

            return file;
        }
    }
}
