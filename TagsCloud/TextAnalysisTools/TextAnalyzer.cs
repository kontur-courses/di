using System.Diagnostics;
using System.Text.Json;
using TagsCloud.Entities;

namespace TagsCloud.TextAnalysisTools;

public static class TextAnalyzer
{
    public static IEnumerable<TextAnalysis> GetTextAnalysis(List<string> textLines)
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "mystem",
                Arguments = "-i --format=json",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            }
        };

        process.Start();
        textLines.ForEach(line => process.StandardInput.Write(line + ' '));
        process.StandardInput.Close();

        var analysis = JsonSerializer.Deserialize<List<TextAnalysis>>(process.StandardOutput.ReadToEnd());
        process.WaitForExit();

        return analysis!;
    }
}