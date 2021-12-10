using System.Collections.Generic;
using NUnit.Framework;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests.Infrastructure.Lemmatizer;

internal class RussianLemmatizerTestCases
{
    public static IEnumerable<TestCaseData> TestCaseDatas
    {
        get
        {
            yield return new TestCaseData(new List<string>
                {
                    null,
                    "",
                    "коровы",
                    "под",
                    "и",
                    "синие",
                    "бегал",
                    "красив",
                    "зелен",
                    "мне",
                    "бегающий",
                    "lambda"
                }, new List<Lemma>
                {
                    new("корова", PartOfSpeech.Noun),
                    new("под", PartOfSpeech.Preposition),
                    new("и", PartOfSpeech.Interjection),
                    new("синий", PartOfSpeech.Adjective),
                    new("бегать", PartOfSpeech.Verb),
                    new("красить", PartOfSpeech.Gerund),
                    new("зелёный", PartOfSpeech.ShortAdjective),
                    new("я", PartOfSpeech.Pronoun),
                    new("бегать", PartOfSpeech.Participle),
                    new("lambda", PartOfSpeech.Unknown)
                })
                .SetName("ReturnCorrectLemmasEnumerable");
        }
    }
}