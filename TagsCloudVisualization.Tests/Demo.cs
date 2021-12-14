using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private const string TestTextPath = @"txt\Test_Облако.txt";
        private const string TestLiteraryTextPath = @"txt\Test_Литературный_текст.txt";
        private const string TestBigTextPath = @"txt\Text_Большой_текст.txt";
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
                "create-cloud",
                "-i", dirTestData + TestLiteraryTextPath,
                "-o", dirTestData + "test.jpeg",
                "-w", "1920",
                "-h", "1080",
                "--bgColor", "Moccasin",
                "--fgColors", "Chartreuse;Indigo;Magenta;Teal;BlueViolet;SlateBlue",
                "--fonts", "Calibri;Cambria;Comic Sans MS",
                "--size", "16",
                "--scatter", "5"
            };
            
            Program.Main(args);
        }

        [Test]
        public void Demo2()
        {
            var args = new[] 
            { 
                "show-demo",
                "-o", dirTestData
            };
            
            Program.Main(args);
        }
    }
}