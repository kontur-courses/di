using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SyntaxTextParser.Architecture
{
    public abstract class ExternalToolUser
    {
        protected readonly string FilePath;
        protected readonly string ToolName;

        protected ExternalToolUser(string filePath, string toolName)
        {
            if(!Directory.Exists(filePath))
                throw new ArgumentException($"Path {filePath} isn't exist");
            if(!File.Exists(Path.Combine(filePath, toolName)))
                throw new ArgumentException($"{toolName} not found in {filePath}");

            FilePath = filePath;
            ToolName = toolName;
        }

        public abstract IEnumerable<string> ParseTextInTool(string text);
    }
}