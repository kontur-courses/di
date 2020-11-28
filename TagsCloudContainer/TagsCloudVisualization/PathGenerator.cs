using System.IO;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class PathGenerator
    {
        public PathGenerator(IDateTimeProvider dateTimeProvider)
        {
            Root = Directory.GetCurrentDirectory();
            DateTimeProvider = dateTimeProvider;
        }

        private string Root { get; }
        private IDateTimeProvider DateTimeProvider { get; }

        public string GetNewFilePath()
        {
            var dateTime = DateTimeProvider.GetDateTimeNow();
            return $"{Root}\\{dateTime:MMddyy-HHmmssffffff}.png";
        }
    }
}