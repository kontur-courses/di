using TagCloudContainer.Readers;
using Xceed.Words.NET;

namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class ReaderShould
    {
        [Test]
        public void ReadFile_WhenFormatIsTxt()
        {
            var reader = new Reader();
            var a = File.Create("file.txt");
            a.Close();
            File.WriteAllText("file.txt", "Nice day bro");
            var text = reader.TxtRead("file.txt");
            text.Should().Be("Nice day bro");
        }

        [TestCase("doc")]
        [TestCase("docx")]
        public void ReadFile_WhenFormatIsNotTxt(string format)
        {
            var reader = new Reader();
            var file = DocX.Create($"file.{format}");
            file.InsertParagraph("Nice day bro");
            file.SaveAs($"file.{format}");
            var text = reader.DocRead($"file.{format}");
            var textFalse = reader.TxtRead($"file.{format}");
            text.Should().Be("Nice day bro" + Environment.NewLine);
            textFalse.Should().NotBe("Nice day bro" + Environment.NewLine);
        }
    }
}
