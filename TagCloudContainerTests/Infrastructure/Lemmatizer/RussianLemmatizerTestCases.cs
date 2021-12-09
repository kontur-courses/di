using System.Collections.Generic;
using NUnit.Framework;
using TagCloud.Infrastructure.Lemmatizer;

namespace TagCloudTests.Infrastructure.Lemmatizer
{
    internal class RussianLemmatizerTestCases
    {
        public static IEnumerable<TestCaseData> TestCaseDatas
        {
            get
            {
                yield return new TestCaseData(null, false, null)
                    .SetName("ReturnNullOnNullString");
                yield return new TestCaseData("", false, null)
                    .SetName("ReturnNullOnEmptyString");
                yield return new TestCaseData("коровы", true, new Lemma("корова", PartOfSpeech.Noun))
                    .SetName("ReturnCorrectLemmaOnRussianNoun");
                yield return new TestCaseData("под", true, new Lemma("под", PartOfSpeech.Preposition))
                    .SetName("ReturnCorrectLemmaOnRussianPreposition");
                yield return new TestCaseData("и", true, new Lemma("и", PartOfSpeech.Interjection))
                    .SetName("ReturnCorrectLemmaOnRussianInterjection");
                yield return new TestCaseData("синие", true, new Lemma("синий", PartOfSpeech.Adjective))
                    .SetName("ReturnCorrectLemmaOnRussianAdjective");
                yield return new TestCaseData("бегал", true, new Lemma("бегать", PartOfSpeech.Verb))
                    .SetName("ReturnCorrectLemmaOnRussianVerb");
                yield return new TestCaseData("красив", true, new Lemma("красить", PartOfSpeech.Gerund))
                    .SetName("ReturnCorrectLemmaOnRussianGerund");
                yield return new TestCaseData("зелен", true, new Lemma("зелёный", PartOfSpeech.ShortAdjective))
                    .SetName("ReturnCorrectLemmaOnRussianShortAdjective");
                yield return new TestCaseData("мне", true, new Lemma("я", PartOfSpeech.Pronoun))
                    .SetName("ReturnCorrectLemmaOnRussianPronoun");
                yield return new TestCaseData("бегающий", true, new Lemma("бегать", PartOfSpeech.Participle))
                    .SetName("ReturnCorrectLemmaOnRussianParticiple");
                yield return new TestCaseData("lambda", true, new Lemma("lambda", PartOfSpeech.Unknown))
                    .SetName("ReturnLemmaOnNonRussianWord");
            }
        }
    }
}
