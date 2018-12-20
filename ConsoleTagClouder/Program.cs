using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TagCloud;    
using CommandLine;
using Extensions;

namespace ConsoleTagClouder
{
    public static class Program
    {public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AppSettings>(args)
                .WithParsed(MakeCloud)
                .WithNotParsed(HandleErrors);
        }

        private static void MakeCloud(AppSettings settings)=>
            Result.Of(() => Cloud.CreateMaker(settings.BuildCloudSettings(), settings.BuildDrawingSettings()))
                .ThenAct(clouder => clouder.UpdateWith(File.ReadAllText(settings.SourcePath)))
                .Then(clouder => clouder.DrawCloud())
                .ThenAct(map => map.Save(settings.TargetPath, ImageFormat.Png))
                .Then(map => map.Dispose())
                .ThenAct(n=>Console.WriteLine("Cloud saved into " + settings.TargetPath))
                .RefineError("Making cloud failed because following errors occured: ")
                .OnFail(Console.WriteLine)
                .OnFail(e => Environment.ExitCode = -1);

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            var sb = new StringBuilder();
            var isParsingContinues = true;
            foreach (var error in errors)
            {
                sb.Append(error);
                isParsingContinues &= !error.StopsProcessing;
            }
            
            if(!isParsingContinues)
                Environment.ExitCode = -1;                

            var message = isParsingContinues
                ? "Following errors occured while parsing command:"
                : "Parsing command failed because following errors occured:"; 
            sb.Insert(0,message);
            Console.WriteLine(sb.ToString());
        } 
    }
    
}