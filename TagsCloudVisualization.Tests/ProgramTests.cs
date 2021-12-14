using System;
using System.Diagnostics;
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
        public void Program_CheckExecutionTime_ForCreateCloud()
        {
            var inputFile = dirTestData + TestLiteraryTextPath;
            var outputFile = dirTestData + "test.png";
            var options = new[] 
            { 
                "create-cloud",
                "-i", inputFile,
                "-o", outputFile,
                "-w", "1920",
                "-h", "1080",
                "--bgColor", "Moccasin",
                "--fgColors", "Chartreuse;Indigo;Magenta;Teal;BlueViolet;SlateBlue",
                "--fonts", "Calibri;Cambria;Comic Sans MS",
                "--size", "25",
                "--scatter", "10"
            };

            var time = CheckExecutionTime(() => Program.Main(options));
            
            Console.WriteLine($"Время выполнения для файла {inputFile} - {time.Elapsed}");
            Console.WriteLine($"Сгенерированное изображение - {outputFile}");
        }

        [Test]
        public void Program_CheckExecutionTime_ForShowDemo()
        {
            var options = new[] 
            { 
                "show-demo",
                "-o", dirTestData
            };
            
            var time = CheckExecutionTime(() => Program.Main(options));
            
            Console.WriteLine($"Время выполнения для команды 'show-demo' - {time.Elapsed}");
        }

        private static Stopwatch CheckExecutionTime(Action action)
        {
            var timer = new Stopwatch();
            timer.Start();
            action.Invoke();
            timer.Stop();

            return timer;
        }
    }
}