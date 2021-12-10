﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TagCloud.Words.Writers;

namespace TagCloud.Words.Preprocessors
{
    public class ToInitFormPreprocessor : IPreprocessor
    {
        private readonly string inputFilePath;
        private readonly string pathToInitFormExe;
        private readonly ILineWriter writer;

        public ToInitFormPreprocessor(ILineWriter writer)
        {
            this.writer = writer;
            inputFilePath = ".stem_input";
            pathToInitFormExe = "mystem.exe";
        }

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            writer.WriteLinesTo(new StreamWriter(inputFilePath, false, Encoding.UTF8), words);

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