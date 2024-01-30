using System.Diagnostics;
using System.Text.Json;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.TextAnalysisTools;

public class TextAnalyzer
{
    public static void FillWithAnalysis(HashSet<WordTagGroup> wordGroups)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "mystem",
            Arguments = "-i --format=json",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        process.Start();

        foreach (var group in wordGroups)
            process.StandardInput.Write(group.WordInfo.Text + ' ');

        process.StandardInput.Close();

        var analyses = JsonSerializer.Deserialize<List<WordSummary>>(process.StandardOutput.ReadToEnd());
        process.WaitForExit();

        var analysisIndex = 0;

        foreach (var group in wordGroups)
        {
            var analysis = analyses[analysisIndex].Analyses.FirstOrDefault();

            if (analysis == null)
            {
                group.WordInfo.IsRussian = false;
            }
            else
            {
                group.WordInfo.Infinitive = analysis.Infinitive;
                group.WordInfo.LanguagePart = analysis.LanguagePart;
            }

            analysisIndex++;
        }
    }
}