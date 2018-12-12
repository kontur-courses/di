using System.Diagnostics;
using System.IO;

namespace TagCloud
{
    public static class ProgramExecuter
    {
        public static string RunProgram(string command, string paramsString, string content)
        {
            using (var process = CreateProcess(command, paramsString))
            {
                process.Start();
                WriteToStream(process, content);
                return ReadFromStream(process);
            }
        }

        public static Process CreateProcess(string command, string paramsString)
        {
            return new Process
            {
                StartInfo =
                {
                    FileName = command,
                    Arguments = paramsString,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                }
            };
        }

        public static void WriteToStream(Process process, string content)
        {
            using (var writer = new StreamWriter(process.StandardInput.BaseStream))
            {
                writer.WriteLine(content);
            }
        }

        public static string ReadFromStream(Process process)
        {
            using (var reader = new StreamReader(process.StandardOutput.BaseStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}