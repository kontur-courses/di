using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TagCloud.Words.Preprocessing
{
    public class ToInitFormPreprocessor : IPreprocessor
    {
        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "stemInput.txt");

            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var word in words) writer.WriteLine(word);
            }

            var initialLeadingFormProcess = CreateInitialLeadingFormProcess(fileName);

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
            var initialFormLeadingProgramExe = Path.Combine(Directory.GetCurrentDirectory(), "mystem.exe");

            return new ProcessStartInfo
            {
                FileName = initialFormLeadingProgramExe,
                Arguments = $"-nld {pathToFile}",
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8
            };
        }
    }
}