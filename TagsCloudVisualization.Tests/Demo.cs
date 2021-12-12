using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private const string TestTextPath = @"txt\Test_Облако.txt";
        private const string TestLiteraryTextPath = @"txt\Test_Литературный_текст.txt";
        private string dirTestData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dirTestData = TestContext.CurrentContext.TestDirectory + @"\TestData\";
        }

        [Test]
        public void Demo()
        {
            var args = new[] 
            { 
                "-i", dirTestData + TestLiteraryTextPath,
                "-o", dirTestData + "test.png",
                "-w", "1920",
                "-h", "1080",
                "--bgColor", "Moccasin",
                "--fgColors", "Chartreuse;Indigo;Magenta;Teal;BlueViolet;SlateBlue",
                "--fonts", "Calibri;Cambria;Comic Sans MS",
                "--size", "40",
                "--scatter", "20"
            };
            
            Program.Main(args);
        }
    }
}