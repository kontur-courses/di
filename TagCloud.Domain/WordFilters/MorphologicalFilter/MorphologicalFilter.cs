using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

public class MorphologicalFilter : IWordsFilter
{
    private string flags = "-lin";
    private string input = "temp.txt";
    private string output = "temp2.txt";
    private string executableFile = "mystem.exe";
    private string executablePath;
    private PartSpeech partsSpeech;

    public MorphologicalFilter(WordExtractionOptions options)
    {
        partsSpeech = options.PartsSpeech;

#if DEBUG
        var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;

        executablePath = Directory.GetFiles(
                Directory.GetParent(projectPath).FullName,
                executableFile, SearchOption.AllDirectories)[0];
#else
        executablePath = executableFile; 
#endif
    }

    public string[] Filter(string[] words)
    {
        File.WriteAllText(input, string.Join(" ", words));

        var process = InitProcess();
        process.Start();
        process.WaitForExit();

        var result = File.ReadAllLines(output);

        File.Delete(input);
        File.Delete(output);

        var parsed = result.Select(ParseResult).ToArray();

        var filtered = parsed.Where(x => partsSpeech.HasFlag(x.partSpeech)).Select(x => x.word).ToArray();

        return filtered;
    }

    private static PartSpeech IdentifyPartSpeech(string alias)
    {
        return alias switch
        {
            "A" => PartSpeech.Adjective,
            "ADV" => PartSpeech.Adverb,
            "ADVPRO" => PartSpeech.PronominalAdverb,
            "ANUM" => PartSpeech.NumeralAdjective,
            "APRO" => PartSpeech.PronounAdjective,
            "COM" => PartSpeech.PartComposite,
            "CONJ" => PartSpeech.Union,
            "INTJ" => PartSpeech.Interjection,
            "NUM" => PartSpeech.Numeral,
            "PART" => PartSpeech.Particle,
            "PR" => PartSpeech.Preposition,
            "S" => PartSpeech.Noun,
            "SPRO" => PartSpeech.PronounNoun,
            "V" => PartSpeech.Verb,
            _ => PartSpeech.None
        };
    }

    (string word, PartSpeech partSpeech) ParseResult(string result)
    {
        var match = Regex.Match(result, @"([А-Яа-я]+)\??=([A-Z]+)");

        return (match.Groups[1].Value, IdentifyPartSpeech(match.Groups[2].Value));
    }

    private Process InitProcess()
    {
        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.Arguments = $"{input} {output} {flags}";
        process.StartInfo.FileName = executablePath;
        return process;
    }
}