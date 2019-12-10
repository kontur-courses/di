using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudLibrary.MyStem
{
    public class MyStemProcess
    {
        private readonly Process myStemProcess;

        private readonly string inputFileName;
        private readonly string outputFileName;

        public MyStemProcess()
        {
            inputFileName = Guid.NewGuid() + ".txt";
            outputFileName = Guid.NewGuid() + ".txt";

            myStemProcess = new Process
            {
                StartInfo =
                {
                    FileName = "exe/mystem.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    Arguments = $"-ni {inputFileName} {outputFileName}"
                }
            };
        }

        private IEnumerable<Word> RunMystemOn(Stream stream)
        {
            using (var fs = File.Create(inputFileName))
            {
                stream.CopyTo(fs);
            }

            myStemProcess.Start();
            myStemProcess.WaitForExit();
            myStemProcess.Close();

            File.Delete(inputFileName);

            var words = new List<Word>();
            
            using (var streamReader = File.OpenText(outputFileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        var word = new Word(line);
                        words.Add(word);
                    }
                    catch (Exception e)
                    {
                        // ignore if word cannot be determined
                    }
                }
            }
            
            File.Delete(outputFileName);

            return words;
        }

        public IEnumerable<string> StreamToWords(Stream stream)
        {
            return RunMystemOn(stream).Select(word => word.Grammar.InitialForm);
        }

        private static Stream StreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        
        public IEnumerable<Word> GetWordsWithGrammar(IEnumerable<string> words)
        {
            return RunMystemOn(StreamFromString(words.Aggregate("\n", (s1, s2) => s1 + "\n" + s2)));
        }
    }
 }