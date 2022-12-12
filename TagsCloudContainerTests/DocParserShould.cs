using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Spire.Doc;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudContainerTests
{
    public class DocParserShould
    {
        public const string FileName = "READ_TEST.doc";
        private Document documentWriter;
        private ParserSettings settings;

        [SetUp]
        public void SetUp()
        {
            File.Create(FileName).Close();

            settings = new ParserSettings();
            documentWriter = new Document();
        }

        [TearDown]
        public void TearDown()
        {
            documentWriter.Close();
            if (File.Exists(FileName))
                File.Delete(FileName);
        }

        [Test]
        public void ReadOneWordOneLineText()
        {
            var words = new[] { "Привет", "Мир" };
            var paragraph1 = documentWriter.AddSection().AddParagraph();
            var paragraph2 = documentWriter.AddSection().AddParagraph();
            paragraph1.Text = words[0];
            paragraph2.Text = words[1];

            documentWriter.SaveToFile(FileName, FileFormat.Doc);
            documentWriter.Close();

            settings.TextType = TextType.OneWordOneLine;
            var parser = new DocParser(settings);
            var result = parser.WordParse(FileName).ToArray()[1..];

            result.Should().BeEquivalentTo(words);
        }

        [Test]
        public void ReadLiteraryText()
        {
            var expected = new[]
            {
                "Скажи", "ка", "дядя", "ведь", "не", "даром",
                "Москва", "спаленная", "пожаром", "Французу", "отдана",
                "Ведь", "были", "ж", "схватки", "боевые",
                "Да", "говорят", "еще", "какие"
            };
            var paragraph1 = documentWriter.AddSection().AddParagraph();
            var paragraph2 = documentWriter.AddSection().AddParagraph();
            paragraph1.Text = @"- Скажи-ка, дядя, ведь не даром
Москва, спаленная пожаром,
	Французу отдана?

";
            paragraph2.Text = @"Ведь были ж схватки боевые,
Да, говорят, еще какие";

            documentWriter.SaveToFile(FileName, FileFormat.Doc);
            documentWriter.Close();

            settings.TextType = TextType.LiteraryText;
            var parser = new DocParser(settings);
            var result = parser.WordParse(FileName).ToArray()[11..];

            result.Should().BeEquivalentTo(expected);
        }
    }
}