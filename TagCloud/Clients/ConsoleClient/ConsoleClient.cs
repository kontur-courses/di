using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudCreator;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.ImageSaver;
using TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

namespace TagCloud.Clients.ConsoleClient
{
    public class ConsoleClient : IClient
    {
        private string? path;
        private string? savePath;
        private Size imageSize;
        private Color bgColor;
        private Color wordColor;

        private readonly ICloudCreator creator;
        private readonly IImageSaver imageSaver;
        private readonly IInputWordsStream inputWordsStream;
        private readonly IWordsPreprocessor wordsPreprocessor;
        private readonly IReadOnlyCollection<IBoringWords> boringWords;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ICloudLayouterSettings cloudLayouterSettings;
        private readonly IDrawingSettings drawingSettings;
        private readonly IWordVisualisation defaultVisualisation;
        private readonly ICloudDrawer cloudDrawer;

        public ConsoleClient(
            IInputWordsStream inputWordsStream,
            IWordsPreprocessor wordsPreprocessor,
            IReadOnlyCollection<IBoringWords> boringWords,
            ICloudLayouter cloudLayouter,
            ICloudLayouterSettings cloudLayouterSettings,
            IDrawingSettings drawingSettings,
            IWordVisualisation defaultVisualisation,
            ICloudDrawer cloudDrawer,
            ICloudCreator cloudCreator,
            IImageSaver imageSaver)
        {
            this.inputWordsStream = inputWordsStream;
            this.wordsPreprocessor = wordsPreprocessor;
            this.boringWords = boringWords;
            this.cloudLayouter = cloudLayouter;
            this.cloudLayouterSettings = cloudLayouterSettings;
            this.drawingSettings = drawingSettings;
            this.defaultVisualisation = defaultVisualisation;
            this.cloudDrawer = cloudDrawer;
            creator = cloudCreator;
            this.imageSaver = imageSaver;
        }

        public void Run()
        {
            Start();

            try
            {
                if (GotFilePath(out path)
                    && GotImageSizes(out imageSize)
                    && GotBgColor(out bgColor)
                    && GotWordsColor(out wordColor)
                    && GotOutImagePath(out savePath))
                {
                    inputWordsStream.SelectFile(path);
                    drawingSettings.BgColor = bgColor;
                    drawingSettings.PictureSize = imageSize;
                    var v = new WordVisualisation(wordColor, 0, new Font("Arial", 3));
                    var image = creator.CreatePicture(inputWordsStream, wordsPreprocessor, boringWords,
                        cloudLayouter, cloudLayouterSettings, drawingSettings, defaultVisualisation, cloudDrawer);

                    Console.WriteLine(!imageSaver.TrySaveImage(image, new FileType(savePath!))
                        ? "Произошла ошибка при сохранении изобразения. Запустите программу ещё раз"
                        : $"Файл сохранён успешно в {savePath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка {e}. Попробуйте перезапустить программу");
            }

            Stop();
        }

        private static void Start()
        {
            Console.WriteLine("Вы запустили создателя облаков тегов.\n" +
                              "Я умею получать слова из файла, в котором они все записаны в столбик");
        }
        
        private static bool GotWordsColor(out Color color)
        {
            Console.WriteLine("Пожалуйста, введите цвет слов на английском\n" +
                              "Для выбора настроек по умолчанию введите black: ");

            if (TryGetColor(out color))
                return true;
            
            Console.WriteLine("Заданный цвет не определён. Повторите попытку? [y/n]: ");
            return Console.ReadLine() == "y" && GotWordsColor(out color);
        }

        private static bool GotBgColor(out Color color)
        {
            Console.WriteLine("Пожалуйста, введите цвет фона на английском\n" +
                              "Для выбора настроек по умолчанию введите white: ");

            if (TryGetColor(out color))
                return true;
            
            Console.WriteLine("Заданный цвет не определён. Повторите попытку? [y/n]: ");
            return Console.ReadLine() == "y" && GotBgColor(out color);
        }

        private static bool TryGetColor(out Color color)
        {
            var stringColor = Console.ReadLine();
            if (stringColor == null)
            {
                color = Color.Empty;
                return false;
            }

            color = Color.FromName(stringColor);
            return color.IsKnownColor;
        }

        private static bool GotImageSizes(out Size size)
        {
            Console.WriteLine("Пожалуйста, введите размеры изображения, которое хотите получить Ш*В (в пикселях)\n" +
                              "Для выбора настроек по умолчанию введите 800*500: ");

            if (TryGetImageSize(out size))
                return true;
            
            Console.WriteLine("Неверный формат записи размеров. Повторите попытку? [y/n]: ");
            return Console.ReadLine() == "y" && GotImageSizes(out size);
        }

        private static bool TryGetImageSize(out Size size)
        {
            var stringSize = Console.ReadLine();
            if (stringSize == null)
            {
                size = new Size(0, 0);
                return false;
            }

            int[] intSize;
            try
            {
                intSize = stringSize.Split('*').Select(int.Parse).ToArray();
            }
            catch
            {
                size = new Size(0, 0);
                return false;
            }

            if (intSize.Length != 2)
            {
                size = new Size(0, 0);
                return false;
            }

            size = new Size(intSize[0], intSize[1]);
            return true;
        }

        private static bool GotOutImagePath(out string? outPath)
        {
            Console.WriteLine("Пожалуйста, введите полный путь к файлу, в который необходимо сохранить изображение\n" +
                              "путь: ");
            outPath = Console.ReadLine();
            return outPath != null;
        }
        
        private static bool GotFilePath(out string filePath)
        {
            Console.WriteLine("Пожалуйста, введите полный путь к вашему файлу со словами\n" +
                              "путь: ");
            if (TryGetFilePath(out var path) && path != null)
            {
                filePath = path;
                return true;
            }
            
            Console.WriteLine("Мне не удалось обнаружить указанный файл. Повторите попытку? [y/n]: ");
            if (Console.ReadLine() == "y") return GotFilePath(out filePath);
            filePath = "";
            return false;
        }

        private static bool TryGetFilePath(out string? filePath)
        {
            var path = Console.ReadLine();
            filePath = path;
            return File.Exists(path);
        }

        private static void Stop()
        {
            Console.WriteLine("Спасибо за использование этой программы\n" +
                              "До свидания!");
        }
    }
}