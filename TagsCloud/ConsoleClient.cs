using System;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class ConsoleClient : IClient
    {
        public string TextFilePath => basePath + textFileName; 
        public string PicFilePath => basePath + picFileName; 
        public string PicFileExtension { get; private set; }

        private readonly string basePath;
        private string textFileName;
        private string picFileName;

        private readonly StringBuilder textFileBuilder;
        private char[] validChars;

        public ConsoleClient(string basePath)
        {
            this.basePath = basePath;
            textFileBuilder = new StringBuilder();
            AddRussianCharsInMassive();
        }

        public void StartClient()
        {
            Console.WriteLine("Enter text file name to save input text:");
            var newLine = Console.ReadLine();
            textFileName = newLine;

            Console.WriteLine("Enter picture file name to save:");
            newLine = Console.ReadLine();
            picFileName = newLine;

            Console.WriteLine("Enter picture file extension to save:");
            newLine = Console.ReadLine();
            PicFileExtension = newLine;

            Console.WriteLine("Enter one word at a time in the stack. If you want to finish, type \"stop\"");

            FillTextFile();
        }

        private bool InputIsValid(string line)
        {
            foreach (var ch in line)
            {
                if (!validChars.Contains(ch) && ch != '\n')
                {
                    return false;
                }
            }

            return true;
        }

        private void AddRussianCharsInMassive()
        {
            char ch;
            int n = 0;
            validChars = new char[64];
            for (int i = 1040; i <= 1103; i++)
            {
                ch = System.Convert.ToChar(i);
                validChars[n] = ch;
                n++;
            }
        }

        private void FillTextFile()
        {
            while (true)
            {
                var newLine = Console.ReadLine();

                if (newLine == "stop")
                {
                    break;
                }

                if (InputIsValid(newLine)) textFileBuilder.Append(newLine + '\n');
                else Console.WriteLine("Invalid word. Please, retype.");
            }

            SaveTextFile();
        }

        private void SaveTextFile()
        {
            using (StreamWriter writer = new StreamWriter(basePath + textFileName, true, System.Text.Encoding.Default))
            {
                var text = textFileBuilder.ToString();
                writer.WriteLine(text);
            }
        }
    }
}
