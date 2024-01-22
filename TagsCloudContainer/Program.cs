using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using TagsCloudContainer.TagsCloud;

namespace TagsCloudContainer
{
    public class Program
    {
           static void Main(string[] args)
        {
            using (var serviceProvider = Startup.ConfigureServices())
            {
                var tagCloudApp = serviceProvider.GetRequiredService<TagCloudApp>();

                tagCloudApp.Run("src/text.doc",         // Исходный файл со словами
                                "src/boring_words.txt", // Файл со скучными словами
                                "Georgia",              // Шрифт текста
                                Color.Black,            // Цвет основного текста
                                Color.Red,              // Цвет популярных слов
                                0.2);                   // % Окрашивания популярных слов (топ-20%)        
            }
        }
    }
}
