using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class Program
    {
        private string inputFileName = "input.txt"; 
        private static readonly Dictionary<string, int> wordsDictionary = new Dictionary<string, int>()
        {
            {"Participation", 90},
            {"Web 2.0", 90 },
            {"Java", 70 },
            {"Usability", 50},
            {"Design", 40},
            {"Standartization", 30},
            {"Python", 20},
            {"Null", 20},
            { "Hello world", 20}
       
        };


        private static List<string> input =
                "Его образованием первоначально занимался гувернёр-француз Сен-Тома́ (прототип St.-Jérôme в повести «Отрочество»), заменивший собою добродушного немца Ресельмана, которого Толстой изобразил в повести «Детство» под именем Карла Ивановича. В 1843 году П. И. Юшкова, взяв на себя роль опекунши своих несовершеннолетних племянников (совершеннолетним был только старший — Николай) и племянницы, привезла их в Казань. Вслед за братьями Николаем, Дмитрием и Сергеем Лев решил поступить в Императорский Казанский университет (наиболее славившийся в то время), где работали на математическом факультете Лобачевский, а на Восточном — Ковалевский. 3 октября 1844 года Лев Толстой был зачислен студентом разряда восточной (арабско-турецкой) словесности в качестве своекоштного — оплачивающего своё обучение[11]. На вступительных экзаменах он, в частности, показал отличные результаты по обязательному для поступления «турецко-татарскому языку». По результатам года имел неуспеваемость по соответствующим предметам, не выдержал переходного экзамена и должен был заново пройти программу первого курса. Во избежание полного повторения курса он перешёл на юридический факультет, где его проблемы с оценками по некоторым предметам продолжились. Переходные майские экзамены 1846 года были сданы удовлетворительно (получил одну пятёрку, три четвёрки и четыре тройки; средний вывод получился три), и Лев Николаевич был переведён на второй курс[12]. На юридическом факультете Лев Толстой пробыл менее двух лет: «Всегда ему было трудно всякое навязанное другими образование, и всему, чему он в жизни выучился, — он выучился сам, вдруг, быстро, усиленным трудом», — пишет С. А. Толстая в своих «Материалах к биографии Л. Н. Толстого»[13]. В 1904 году он вспоминал: «…я первый год … ничего не делал. На второй год я стал заниматься … там был профессор Мейер, который … дал мне работу — сравнение „Наказа“ Екатерины с Esprit des lois <«Духом законов» (фр.)русск.> Монтескьё. … меня эта работа увлекла, я уехал в деревню, стал читать Монтескьё, это чтение открыло мне бесконечные горизонты; я стал читать Руссо и бросил университет, именно потому, что захотел заниматься"
            .Split(new char[] { ' ', '\t', ',', ';', '?', '\n', '.'}, StringSplitOptions.RemoveEmptyEntries).ToList();
        
        
        static void Main(string[] args)
        {
            
            var frequencyDict = new FileParser(50).GetWordsFrequensy(input);
        
            
//            var cloudCenter = new Point(400, 400);
//            var layout = new CircularCloudLayouter(cloudCenter);
//            var tagsDict = new Dictionary<Rectangle, (string, Font)>();
//
//            foreach (var word in frequencyDict)
//            {
//                var font = new Font(new FontFamily("Tahoma"), word.Value, FontStyle.Regular, GraphicsUnit.Pixel);
//                var tagSize = TextRenderer.MeasureText(word.Key,font);
//                tagsDict.Add(layout.PutNextRectangle(tagSize), (word.Key, font));
//                rectangleList.Add(layout.PutNextRectangle(tagSize));
//            }
//
//            CloudTagDrawer.DrawTagsToFile(cloudCenter, tagsDict, "1.bmp", 800, 800);
//            CloudTagDrawer.DrawTagsToForm(cloudCenter, tagsDict ,800, 800);

        }
    }
}
