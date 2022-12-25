using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class BoringWordsDeleter 
    {
       // private readonly HashSet<PartsOfSpeech> _boringPartsOfSpeeches;
        private readonly Regex _partOfSpeechRegex = new Regex(@"{(\w+)=(?<partOfSpeech>\w+)", RegexOptions.Compiled);

        //public PartsOfSpeechFilter(params PartsOfSpeech[] boringPartsOfSpeeches)
       //// {
       //     _boringPartsOfSpeeches = boringPartsOfSpeeches.ToHashSet();
       // }

        public static IEnumerable<string> DeleteBoringWords(IEnumerable<string> words)
        {
            var result = new List<string>();
            var partsOfSpeech = new List<string>
                {"A", "ADV", "ADVPRO", "ANUM", "APRO", "COM", "CONJ", "NUM", "PART", "PR", "S", "SPRO", "V"};
            var boringPartsOfSpeech = new List<string> {"NUM"};
            var myStemProcess = new Process
            {
                StartInfo =
                {
                    FileName = "mystem.exe",
                    Arguments = "-i -n -e cp866",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            

            myStemProcess.Start();
            foreach (var word in words)
            {
                myStemProcess.StandardInput.Write($"{word}\n");
                var wordInfo = myStemProcess.StandardOutput.ReadLine();
                result.Add(word);
                if (wordInfo is null)
                    continue;
                var partOfSpeech = partsOfSpeech.First(p => wordInfo.IndexOf(p, StringComparison.Ordinal) != -1);
                if(boringPartsOfSpeech.Contains(partOfSpeech))
                    continue;
                result.Add(word);
            }

            myStemProcess.StandardInput.Close();
            myStemProcess.WaitForExit();
            myStemProcess.Close();
            myStemProcess.Dispose();

            return result;
        }
    }
}