using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TagsCloudContainer.FileManager;
using TagsCloudContainer.TokensGenerator;

namespace TagsCloudContainer.Filters
{
    public class MyStemFilter : MyStem, IFilter
    {
        private readonly WordType[] allowedWorldType;

        public MyStemFilter(IFileManager fileManager, WordType[] allowedWorldType, string pathToMyStem = null) : base(fileManager, pathToMyStem)
        {
            this.allowedWorldType = allowedWorldType;
        }
        
        public IEnumerable<string> Filtering(IEnumerable<string> tokens)
        {
            var inputFilePath = FileManager.MakeFile();
            var outputFilePath = FileManager.MakeFile();

            FileManager.WriteInFile(inputFilePath, string.Join("\r\n", tokens));
            RunMyStem(inputFilePath, outputFilePath, $"--format json -dgin");


            var strFile = FileManager.ReadFile(outputFilePath);
            var res = strFile.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(JObject.Parse)
                .Select(Token.FromJson)
                .Where(t => allowedWorldType.Contains(t.WordType));

            return res.Select(t => t.Value).Where(s => s.Length > 3);
        }
    }
}