using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace TagsCloudVisualization;

public class MystemWordsParser : IInterestingWordsParser
{
    private IDullWordChecker dullWordChecker;
    
    public MystemWordsParser(IDullWordChecker dullWordChecker)
    {
        this.dullWordChecker = dullWordChecker;
    }
    
    public IEnumerable<string> GetInterestingWords(string path)
    {
        return ParseInterestingWords(path)
            .Where(analysis => !dullWordChecker.Check(analysis))
            .Select(analysis => analysis.Lexema.ToLower());

    }

    private static IEnumerable<WordAnalysis> ParseInterestingWords(string path)
    {
        var readText = File.ReadAllText(path, Encoding.UTF8);
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "mystem.exe"),
                Arguments = "-lig --format json",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                StandardInputEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8,
            }
        };
        process.Start();
        process.StandardInput.Write(readText);
        process.StandardInput.Close();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var wordsAnalysis = new List<WordAnalysis>();
        
        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine();
            var deserializedLine = JsonSerializer.Deserialize<List<JsonWordAnalysis>>(line, options);
            foreach (var jsonWordAnalysis in deserializedLine)
            {
                if (jsonWordAnalysis.Analysis.Count < 1)
                    continue;
                
                var unpackedAnalysis = jsonWordAnalysis.Analysis.First();
                wordsAnalysis.Add(new WordAnalysis(jsonWordAnalysis.Text, unpackedAnalysis["lex"],
                    unpackedAnalysis["gr"]));
            }
        }

        process.WaitForExit();
        return wordsAnalysis;
    }

    private class JsonWordAnalysis
    {
        public string Text { get; set; }
        public List<Dictionary<string, string>> Analysis { get; set; }
    }
}