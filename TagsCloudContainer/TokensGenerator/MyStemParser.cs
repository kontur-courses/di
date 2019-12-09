using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TagsCloudContainer.FileManager;

namespace TagsCloudContainer.TokensGenerator
{
    public class MyStemParser : MyStem, ITokensParser
    {
        public MyStemParser(IFileManager fileManager, string pathToMyStem = null) : base(fileManager, pathToMyStem)
        {
        }

        public IEnumerable<string> GetTokens(string str)
        {
            if (str == null)
                throw new ArgumentNullException();
            var inputFilePath = FileManager.MakeFile();
            var outputFilePath = FileManager.MakeFile();

            FileManager.WriteInFile(inputFilePath, str);

            RunMyStem(inputFilePath, outputFilePath, $"--format json -dn");


            var strFile = FileManager.ReadFile(outputFilePath);
            var res = strFile.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => JObject.Parse(s)["analysis"].FirstOrDefault()?["lex"]);

            return res.Where(s => s != null).Select(s => s.ToString());
        }
    }
}