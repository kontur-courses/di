using System.Text.RegularExpressions;

namespace TagCloud
{
    public class Reader
    {
        public readonly DocReader docReader;
        public readonly TxtReader txtReader;

        public Reader(DocReader docReader, TxtReader txtReader)
        {
            this.docReader = docReader;
            this.txtReader = txtReader;
        }

        public string ReadTextFromFile()
        {
            if (Regex.IsMatch(PathToFile, @"\w*.doc"))
                return docReader.ReadAllText(PathToFile);
            else
                return txtReader.ReadAllText(PathToFile);
        }

        public string PathToFile { get; set; }

    }
}
