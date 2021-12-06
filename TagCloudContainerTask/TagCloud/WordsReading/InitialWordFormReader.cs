using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TagCloud.WordsReading
{
    public class InitialWordFormReader : IReader
    {
        private const string DefaultStemArguments = "-nld";

        private readonly string solutionDirectory
            = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;

        public IEnumerable<string> ReadWordsFromFile(string pathToFile)
        {
            var initialLeadingFormProcess = CreateInitialLeadingFormProcess(pathToFile);

            using (initialLeadingFormProcess)
            {
                initialLeadingFormProcess.Start();
                while (!initialLeadingFormProcess.StandardOutput.EndOfStream)
                    yield return initialLeadingFormProcess.StandardOutput.ReadLine();
            }
        }

        private Process CreateInitialLeadingFormProcess(string pathToFile)
        {
            return new Process
            {
                StartInfo = BuildStartInfo(pathToFile)
            };
        }

        private ProcessStartInfo BuildStartInfo(string pathToFile)
        {
            var initialFormLeadingProgramExe = Path.Combine(solutionDirectory, "mystem.exe");

            return new ProcessStartInfo
            {
                FileName = initialFormLeadingProgramExe,
                Arguments = $"{DefaultStemArguments} {pathToFile}",
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8
            };
        }
    }
}