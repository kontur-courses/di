using Spire.Doc;

namespace TagsCloudTests;

public static class FileReaderTestData
{
    public static IEnumerable<TestCaseData> ConstructSomeFileTypes => new[]
    {
        new TestCaseData("TEST.txt", FileFormat.Txt, "They \n call", new[] { "they", "call" }).SetName(
            "WhenReadTxtFile"),
        new TestCaseData("TEST.doc", FileFormat.Doc, "They \n call", new[] { "they", "call" }).SetName(
            "WhenReadDocFile"),
        new TestCaseData("TEST.docx", FileFormat.Docx, "ask \n cash \n out", new[] { "ask", "cash", "out" }).SetName(
            "WhenReadDocxFile"),
        new TestCaseData("litr.txt", FileFormat.Txt, "Straight from .the. hip, cut to the chase\n" +
                                                       "Play? no games, say no names",
            new[]
            {
                "straight", "from", "the", "hip", "cut", "to", "the", "chase", "play", "no", "games", "say", "no",
                "names"
            }).SetName("WhenReadLiteratureText")
    };
}