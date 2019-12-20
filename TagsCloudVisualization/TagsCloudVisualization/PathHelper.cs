using System;
using System.Text.RegularExpressions;
using TagsCloudVisualization.Results;

namespace TagsCloudVisualization
{
    internal class PathHelper
    {
        public static Result<string> ResourcesPath
        {
            get
            {
                var projectPathRegex = new Regex(@".*TagsCloudVisualization");
                var match = projectPathRegex.Match(Environment.CurrentDirectory);
                if (!match.Success)
                    return Result.Fail<string>($"Can't get parent directory for {Environment.CurrentDirectory} ");
                return match.Value + "\\Resources";
            }
        }
    }
}