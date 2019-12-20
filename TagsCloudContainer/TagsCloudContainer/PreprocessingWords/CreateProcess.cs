using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TagsCloudContainer.PreprocessingWords
{
    public class CreateProcess : ICreateProcess
    {
        public IEnumerable<string> GetResult(string nameProgram, string arguments)
        {
            var words = new List<string>();
            using (var process = new Process
            {
                StartInfo =
                {
                    FileName = nameProgram,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.UTF8
                }
            })
            {
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    var word = process.StandardOutput.ReadLine();
                    if (word != null)
                        words.Add(word);
                }
            }

            return words;
        }
    }
}