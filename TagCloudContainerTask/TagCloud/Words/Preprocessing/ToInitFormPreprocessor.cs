using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TagCloud.Words.Writing.ToFile;

namespace TagCloud.Words.Preprocessing
{
    public class ToInitFormPreprocessor : IPreprocessor
    {
        private readonly IFileWriter fileWriter;
        private readonly string inputFilePath;
        private readonly string pathToInitFormExe;

        public ToInitFormPreprocessor(IFileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
            inputFilePath = ".stem_input";
            pathToInitFormExe = "mystem.exe";
        }

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            fileWriter.WriteToFile(inputFilePath, words, Encoding.UTF8);

            var initialLeadingFormProcess = CreateInitialLeadingFormProcess(inputFilePath);

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
            return new ProcessStartInfo
            {
                FileName = pathToInitFormExe,
                Arguments = $"-nld {pathToFile}",
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8
            };
        }
    }
}