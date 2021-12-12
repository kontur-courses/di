using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class CloudSettingsAction : IUIAction
    {
        private Settings settings;
        private CloudSettings cloudSettings;

        public CloudSettingsAction(Settings settings, CloudSettings cloudSettings)
        {
            this.settings = settings;
            this.cloudSettings = cloudSettings;
        }

        public string GetDescription() => "Cloud settings";

        public void Handle()
        {
            Console.WriteLine("1 - Tag painting");
            Console.WriteLine("2 - Cloud view");
            Console.WriteLine("3 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            switch (answer.KeyChar)
            {
                case '1':
                    TagPaintingKey();
                    break;
                case '2':
                    CloudViewKey();
                    break;
                default:
                    return;
            }
        }

        private void TagPaintingKey()
        {
            Console.WriteLine("1 - Primary");
            Console.WriteLine("2 - Frequency");
            Console.WriteLine("3 - Gradient");
            Console.WriteLine("4 - Random");
            Console.WriteLine("5 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            switch (answer.KeyChar)
            {
                case '1':
                    cloudSettings.Painter = new PrimaryTagPainter(settings);
                    break;
                case '2':
                    cloudSettings.Painter = new FrequencyTagPainter(settings);
                    break;
                case '3':
                    cloudSettings.Painter = new GradientTagPainter(settings);
                    break;
                case '4':
                    cloudSettings.Painter = new RandomTagPainter();
                    break;
                default:
                    return;
            }
        }

        private void CloudViewKey()
        {
            Console.WriteLine("1 - Circle");
            Console.WriteLine("2 - Oval");
            Console.WriteLine("3 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            switch (answer.KeyChar)
            {
                case '1':
                    cloudSettings.Spiral = new ArchimedeanSpiral(settings);
                    break;
                case '2':
                    cloudSettings.Spiral = new OvalSpiral(settings);
                    break;
                default:
                    return;
            }
        }
    }
}
