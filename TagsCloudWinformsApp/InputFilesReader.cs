using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;

namespace TagsCloudWinformsApp
{
    internal class InputFilesReader : IWordSequenceProvider, IWordFilterProvider
    {
        public IEnumerable<string> WordSequence
        {
            get
            {
                var wordSeq = new List<string>();
                foreach (var line in File.ReadAllLines(InputPath)) wordSeq.AddRange(line.Split());
                return wordSeq;
            }
        }

        public IEnumerable<string> WordFilter
        {
            get
            {
                if (FilterPath == null) return new List<string>();
                var wordSeq = new List<string>();
                foreach (var line in File.ReadAllLines(FilterPath)) wordSeq.AddRange(line.Split());
                return wordSeq;
            }
        }

        public string? InputPath = null;
        public string? FilterPath = null;
    }
}