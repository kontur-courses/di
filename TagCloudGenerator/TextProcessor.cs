namespace TagCloudGenerator
{
    public class TextProcessor : ITextProcessor
    {
        public IEnumerable<string> ProcessText(IEnumerable<string> text)
        {
            //var mystem = new Process();
            //mystem.StartInfo.WorkingDirectory = "C:\\Users\\lholy\\Documents\\GitHub\\di\\TagCloudGeneratorTest\\TestsData";
            //mystem.StartInfo.FileName = "test1.txt";
            //mystem.StartInfo.Arguments = "-n input.txt output.txt";

            //mystem.Start();
            //StreamReader mystemStreamReader = mystem.StandardOutput;
            //string bs = mystemStreamReader.ReadToEnd();

            //var outputText = " ";
            //StreamWriter mystemStreamWriter = mystem.StandardInput;
            //outputText += mystemStreamReader.ReadToEnd() + " ";
            //mystem.WaitForExit();
            //mystem.Close();

            //hunspell
            //var words2 = new List<string>() { "мама", "мыла", "раму" };
            //var words = new List<string>() {"draw", "cat", "beautiful" };
            //var dictionary = WordList.CreateFromFiles(@"Russian.aff");


            ////var wordList = WordList.CreateFromFiles(
            ////    //@"English (American).dic",
            ////    //@"English (American).aff");
            ////    @"Russian.aff",
            ////    @"Russian.aff");
            ////var initialWord = "ленись";
            ////var details = wordList.CheckDetails(initialWord);
            ////var wordEntryDetails = wordList[string.IsNullOrEmpty(details.Root) ? initialWord : details.Root];
            ////var po = wordEntryDetails[0].Morphs[0];

            //var chtoto = dictionary.RootWords;

            //var a = chtoto.ToImmutableArray();
            //bool notOk = dictionary.Check("Color");
            ////var suggestions = dictionary.Suggest("Color");
            //bool ok = dictionary.Check("Colour");
            //WordCounter wordCounter = new WordCounter();
            //wordCounter.CountWords(file);

            foreach (string line in text)
                yield return line.ToLower();
            //bool c = dictionary.Check(words[i]);
            //var spellCheckResult = dictionary.Check(words[i]);
            //var t = dictionary.Suggest(words[i]);

            //var warnings = dictionary.Affix.Warnings;

            //var suggestions = dictionary.Suggest(file[i]);

            // var cd = dictionary.CheckDetails(words[i]);

        }
    }
}