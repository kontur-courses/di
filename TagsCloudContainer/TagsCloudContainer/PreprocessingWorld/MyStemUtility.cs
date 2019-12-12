using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.PreprocessingWorld
{
    public class MyStemUtility : IPreprocessingWorld
    {
        private readonly string pathMyStemUtility;
        private readonly string flags;
        
        public MyStemUtility(string pathMyStemUtility)
        {
            this.pathMyStemUtility = pathMyStemUtility;
            flags = "-nig --format json";
        }


        public string GetResult(string filePath)
        {
            var bufer = new StringBuilder();
            using (var proc = CreateProcess(filePath))
            {
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    bufer.AppendLine(proc.StandardOutput.ReadLine());
                }
            }
            return bufer.ToString();
        }
        
        private Process CreateProcess(string filePath)
        {
            return new Process
            {
                StartInfo =
                {
                    FileName = pathMyStemUtility,
                    Arguments = flags + " " + filePath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.UTF8
                }
            };
        }

        public IEnumerable<string> Preprocessing(IEnumerable<string> strings)
        {
            throw new System.NotImplementedException();
        }
    }
}