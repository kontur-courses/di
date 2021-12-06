using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TagCloud.Extensions;

namespace TagCloud.WordsPreprocessing
{
    public class InitialWordFormReader : IReader
    {
        private const string DefaultStemArguments = "-nld";

        private readonly string solutionDirectory
            = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;

        public IEnumerable<string> ReadWordsFromFile(string pathToFile)
        {
            var initialLeadingFormProcess = CreateInitialLeadingFormProcess(pathToFile);

            var words = initialLeadingFormProcess.ReadStandardOutput();

            return words;
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