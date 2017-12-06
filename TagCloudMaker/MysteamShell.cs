using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace TagCloud
{
    public static class MysteamShell
    {
        public static List<string> Analyze(string filePath)
        {
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
            return analyzeResult;
        }
    }
}