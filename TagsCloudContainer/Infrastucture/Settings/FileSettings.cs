namespace TagsCloudContainer.Infrastucture.Settings
{
    public class FileSettings
    {
        private string imagePath = GetProjectDirectory() + @"\";
        private string sourceFilePath = GetProjectDirectory() + @"\src\sourceWords.txt";
        private string boringFilePath = GetProjectDirectory() + @"\src\boringWords.txt";

        public string ImagePath
        {
            get => imagePath;
            set => imagePath = File.Exists(value) ? value : imagePath;
        }

        public string SourceFilePath
        {
            get => sourceFilePath;
            set => sourceFilePath = File.Exists(value) ? value : sourceFilePath;
        }

        public string BoringFilePath
        {
            get => boringFilePath;
            set => boringFilePath = File.Exists(value) ? value : boringFilePath;
        }

        private static string GetProjectDirectory()
        {
            var binDirectory = AppContext.BaseDirectory;
            var projectDirectory = Directory.GetParent(binDirectory).Parent.Parent.Parent.FullName;

            return projectDirectory;
        }
    }
}