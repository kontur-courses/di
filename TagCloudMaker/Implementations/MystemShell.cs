using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class MystemShell: IMystemShell
    {
        public Result<IEnumerable<string>> GetInterestingWords(string filePath)
        {
            if (!File.Exists(filePath))
                return Result.Fail<IEnumerable<string>>($"File {filePath} doesn't exists.");
            if(!File.Exists("mystem.exe"))
                return Result.Fail<IEnumerable<string>>($"Place mystem.exe into {AppDomain.CurrentDomain.BaseDirectory}.");

            var analyzeResult = new List<string>();
            using (var mystem = new Process())
            {
                mystem.StartInfo.FileName = "mystem.exe";
                mystem.StartInfo.Arguments = $"-ni {filePath}";
                mystem.StartInfo.UseShellExecute = false;
                mystem.StartInfo.RedirectStandardOutput = true;
                mystem.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                mystem.Start();

                while(!mystem.StandardOutput.EndOfStream)
                    analyzeResult.Add(mystem.StandardOutput.ReadLine());

                mystem.WaitForExit();
                mystem.Close();
            }
            return Result.Ok((IEnumerable<string>)analyzeResult);
        }
    }
}