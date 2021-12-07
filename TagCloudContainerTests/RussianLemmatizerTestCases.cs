using System.Collections.Generic;
using NUnit.Framework;
using TagCloudContainer.Infrastructure.Lemmatizer;

namespace TagCloudContainerTests
{
    internal class RussianLemmatizerTestCases
    {
        public static IEnumerable<TestCaseData> TestCaseDatas
        {
            get
            {
                yield return new TestCaseData(null, null)
                    .SetName("OutNullOnNullString");
                yield return new TestCaseData("", null)
                    .SetName("OutNullOnEmptyString");
                yield return new TestCaseData("коровы", new Lemma("корова", PartOfSpeech.Noun))
                    .SetName("OutCorrectLemmaOnRussianNoun");
                yield return new TestCaseData("под", new Lemma("под", PartOfSpeech.Preposition))
                    .SetName("OutCorrectLemmaOnRussianPreposition");
                yield return new TestCaseData("и", new Lemma("и", PartOfSpeech.Interjection))
                    .SetName("OutCorrectLemmaOnRussianInterjection");
                yield return new TestCaseData("синие", new Lemma("синий", PartOfSpeech.Adjective))
                    .SetName("OutCorrectLemmaOnRussianAdjective");
                yield return new TestCaseData("бегал", new Lemma("бегать", PartOfSpeech.Verb))
                    .SetName("OutCorrectLemmaOnRussianVerb");
                yield return new TestCaseData("красив", new Lemma("красить", PartOfSpeech.Gerund))
                    .SetName("OutCorrectLemmaOnRussianGerund");
                yield return new TestCaseData("зелен", new Lemma("зелёный", PartOfSpeech.ShortAdjective))
                    .SetName("OutCorrectLemmaOnRussianShortAdjective");
                yield return new TestCaseData("мне", new Lemma("я", PartOfSpeech.Pronoun))
                    .SetName("OutCorrectLemmaOnRussianPronoun");
                yield return new TestCaseData("бегающий", new Lemma("бегать", PartOfSpeech.Participle))
                    .SetName("OutCorrectLemmaOnRussianParticiple");
                yield return new TestCaseData("lambda", new Lemma("lambda", PartOfSpeech.Unknown))
                    .SetName("OutLemmaOnNonRussianWord");
            }
        }
    }
}
