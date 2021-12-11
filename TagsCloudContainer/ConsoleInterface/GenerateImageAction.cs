using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class GenerateImageAction
    {
        private Settings settings;
        private IParser parser;
        private IPreprocessorsApplier preprocessorsApplier;
        IFiltersApplier filtersApplier;
        private ITagCreator creator;
        private CloudSettings cloudSettings;

        public GenerateImageAction(Settings settings, IParser parser,
        IPreprocessorsApplier preprocessorsApplier,
        IFiltersApplier filtersApplier, ITagCreator creator,
        CloudSettings cloudSettings)
        {
            this.settings = settings;
            this.parser = parser;
            this.preprocessorsApplier = preprocessorsApplier;
            this.filtersApplier = filtersApplier;
            this.creator = creator;
            this.cloudSettings = cloudSettings;
        }

        public void Handle()
        {
            var textsPath = Path.GetFullPath(@"..\..\..\texts");
            Console.WriteLine($"Texts path is {textsPath}");
            Console.WriteLine("Enter file from this folder as");
            Console.WriteLine("NAME.FORMAT");
            Console.WriteLine("Or pass an empty string to be brought back to menu");
            Console.WriteLine("Supported formats are txt, doc and docx");
            var answer = Console.ReadLine() ?? "";
            Console.WriteLine();
            var split = answer.Split('.');

            if (split.Length != 2)
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            switch (split[1])
            {
                case "docx":
                    parser = new DocParser();
                    break;
                case "doc":
                    parser = new DocParser();
                    break;
                case "txt":
                    parser = new TxtParser();
                    break;
                default:
                    Console.WriteLine("File format is not supported!");
                    return;
            }

            PaintSource(Path.Combine(textsPath, answer));
        }

        private void PaintSource(string sourcePath)
        {
            if (!File.Exists(sourcePath))
                Console.WriteLine("File not found!");
            else
            {
                Console.WriteLine("The path for image");
                Console.WriteLine(GenerateImage(sourcePath));
            }

            Console.WriteLine();
        }

        private string GenerateImage(string path)
        {
            var parsed = parser.Parse(path);
            var preprocessed = preprocessorsApplier.ApplyPreprocessors(parsed);
            var filtered = filtersApplier.ApplyFilters(preprocessed);
            var tags = creator.CreateTags(filtered);
            var painted = cloudSettings.Painter.Paint(tags);

            var layouter = new OvalCloudLayouter(settings, cloudSettings.Spiral);
            var cloudPainter = new TagCloudPainter(layouter, settings);
            return cloudPainter.Paint(painted);
        }
    }
}
