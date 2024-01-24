namespace TagCloudGenerator
{
    public class TextProcessor : ITextProcessor
    {
        public string[] ProcessText(string[] file)
        {
            for (var i = 0; i < file.Length; i++)
            {             
                file[i] = file[i].ToLower();
            }

            return file;
        }
    }
}