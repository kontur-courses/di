using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.RegularExpressions;
using MyStemWrapper.Domain;
using MyStemWrapper.Domain.Settings;

namespace MyStemWrapper;

public class MyStemWordsGrammarInfoParser
{
    private MyStem _myStem;

    public MyStemWordsGrammarInfoParser(string myStemExecutablePath)
    {
        _myStem = new MyStem(
            new MyStemSettings
            {
                MyStemAppPath = myStemExecutablePath,
                LaunchOptions = new[]
                {
                    MyStemLaunchOption.LinearMode,
                    MyStemLaunchOption.MissOriginalForm,
                    MyStemLaunchOption.AddGrammarInfo
                },
                OutputFormat = MyStemOutputFormat.Json,
                Encoding = MyStemEncoding.Utf8
            }
        );
    }

    public IEnumerable<WordGrammarInfo> Parse(IEnumerable<string> words)
    {
        return _myStem.Analyze(words)
            .Select(jsonRaw => JsonSerializer.Deserialize<RawWordAnalysisResult>(jsonRaw)!)
            .Select(ParseGrammarInfo);
    }

    private WordGrammarInfo ParseGrammarInfo(RawWordAnalysisResult rawAnalysisResult)
    {
        var sourceWord = rawAnalysisResult.Word;
        var grammarResult = rawAnalysisResult.Guesses?
            .Where(rawInfo => rawInfo.Grammar is not null && rawInfo.Lexeme is not null)
            .FirstOrDefault();
        var originalForm = grammarResult?.Lexeme ?? sourceWord;
        var speechPart = TryGetSpeechPart(grammarResult?.Grammar ?? string.Empty, out var result)
            ? result.Value
            : SpeechPart.Undefined;

        return new WordGrammarInfo(sourceWord, originalForm, speechPart);
    }

    private bool TryGetSpeechPart(string myStemSourceGrammar, [NotNullWhen(true)] out SpeechPart? speechPart)
    {
        speechPart = default;
        if (string.IsNullOrWhiteSpace(myStemSourceGrammar))
            return false;
        var speechPartSource = Regex.Split(myStemSourceGrammar, "[=,]")
            .FirstOrDefault(token => !string.IsNullOrWhiteSpace(token));
        if (speechPartSource is null)
            return false;
        speechPart = ParseMyStemSpeechPart(speechPartSource);
        return speechPart is not SpeechPart.Undefined;
    }

    private SpeechPart ParseMyStemSpeechPart(string myStemSpeechPartSource) =>
        myStemSpeechPartSource switch
        {
            "A" => SpeechPart.Adjective,
            "ADV" => SpeechPart.Adverb,
            "ADVPRO" => SpeechPart.PronominalAdverb,
            "ANUM" => SpeechPart.NumeralAdjective,
            "APRO" => SpeechPart.PronominalAdjective,
            "COM" => SpeechPart.CompositeWordPart,
            "CONJ" => SpeechPart.Union,
            "INTJ" => SpeechPart.Interjection,
            "NUM" => SpeechPart.Numeral,
            "PART" => SpeechPart.Particle,
            "PR" => SpeechPart.Preposition,
            "S" => SpeechPart.Noun,
            "SPRO" => SpeechPart.Pronoun,
            "V" => SpeechPart.Verb,
            _ => SpeechPart.Undefined
        };
}