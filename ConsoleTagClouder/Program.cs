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
    {
        private static bool isParsingContinues;
        
        public static void Main(string[] args)
        {
            var settings = ParseSettings(args);
            if (!isParsingContinues)
                return;

            try
            {
                MakeCloud(settings);
            }
            catch (Exception e)
            {                
                Console.WriteLine("Making cloud failed because following errors occured:");
                Console.WriteLine(e);
                throw;
            }
        }

        private static void MakeCloud(AppSettings settings)
        {
            var clouder = Cloud.CreateMaker(settings.BuildClouderSettings());
            clouder.UpdateWith(File.ReadAllText(settings.SourcePath));
            using (var map = clouder.DrawCloud())
                map.Save(settings.TargetPath,ImageFormat.Png);
        }

        public static AppSettings ParseSettings(string[] args)
        {
            AppSettings settings = null;
            Parser.Default.ParseArguments<AppSettings>(args)
                .WithParsed(s => settings = s)
                .WithNotParsed(HandleErrors);
            return settings;
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {    //Alot of code to avoid multiple enumeration.
            var sb = new StringBuilder();
            foreach (var error in errors)
            {
                sb.Append(error);
                isParsingContinues &= !error.StopsProcessing;
            }

            var message = isParsingContinues
                ? "Following errors occured while parsing command:"
                : "Parsing command failed because following errors occured:"; 
            sb.Insert(0,message);
            Console.WriteLine(sb.ToString());
        } 
    }
    
}