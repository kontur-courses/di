using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudContainer.Extensions;
using CommandLine;
using Autofac;

namespace TagsCloudContainer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args).Value;
            var center = new Point(options.Width / 2, options.Height / 2);

            var builder = new ContainerBuilder();
            builder.Register(c => new MyStem(options.InputFile)).AsSelf().SingleInstance();
            builder.Register(c => new ArchimedeanSpiral(center,
                options.StepX, options.StepY, options.StepAngle))
                .As<IDistribution>()
                .SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.Register(c => new ImageCreator(options.Width, options.Height))
                .AsSelf().SingleInstance();
            builder.RegisterType<CloudItem>().As<ICloudItem>();

            var container = builder.Build();
            RunApp(options, container);
        }

        static void RunApp(Options options, IContainer container)
        {
            var ms = container.Resolve<MyStem>();
            var partSpeech = MyStem.GetPartSpeech(options.PartSpeeches);
            var words = ms.GetWords().Where(word => partSpeech.HasFlag(word.PartSpeech));
            var counterWords = new Counter<Word>(words).GetMostPopular(options.Count);

            var cloud = container.ResolveOptional<ICloudLayouter>();
            var imageCreator = container.Resolve<ImageCreator>();

            foreach (var (Word, Amount) in counterWords)
            {
                var fontSize = Utilities.CalcFontSize(options.MinFontSize, options.MaxFontSize, 
                    Amount, counterWords.Last().Amount, counterWords.First().Amount);
                var font = new Font(FontFamily.GenericSansSerif, fontSize);
                var size = imageCreator.Graphics.MeasureString(Word.Text, font).Ceiling();
                cloud.PutNextCloudItem(Word.Text, size, font);
            }

            CreateImage(options, imageCreator, cloud);
        }

        public static void CreateImage(Options options, 
            ImageCreator imageCreator, ICloudLayouter cloud)
        {
            var projectPath = Utilities.GetProjectPath();
            var fullPath = Path.Combine(projectPath, options.OutputFile);
            var foregroundColor = Color.FromName(options.ForegroundColor);
            var backgroundColor = Color.FromName(options.BackgroundColor);
            imageCreator.DrawCloud(cloud.Items, foregroundColor, backgroundColor);
            imageCreator.Save(fullPath);
        }
    }
}