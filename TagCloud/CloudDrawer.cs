using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Extensions;
using TagsCloudVisualization;


namespace TagCloud
{
    public class CloudDrawer
    {
        private readonly ICloudLayouter layouter;
        private readonly DrawingSettings settings;

        //TODO Tests?
        public CloudDrawer(ICloudLayouter layouter, DrawingSettings settings)
        {
            this.layouter = layouter;
            this.settings = settings;
        }

        public Result<Bitmap> Draw(IEnumerable<(string word, int fontSize)> wordWeightPairs)=>
            Result.Of(() => new Bitmap(settings.Size.Height, settings.Size.Width))
                .ThenAct(map =>
                    Result.Of(() => Graphics.FromImage(map))
                        .ThenAct(g => g.FillRegion(settings.BackgroundBrush, g.Clip)) //Maximum functionality
                        .ThenAct(g => wordWeightPairs.Aggregate(
                                            Result.Ok(), 
                                            (res, t) => res.Then(n => DrawWord(g, t))))
                        .Then(g => g.Dispose()));
            
//            var mapa = new Bitmap(settings.Size.Height,settings.Size.Width);
//            using (var g = Graphics.FromImage(mapa))
//            {
//                g.FillRegion(settings.BackgroundBrush, g.Clip);
//                foreach (var t in wordWeightPairs)
//                {
//                    var result = Result.Of(() => (t.word, new Font(settings.FontType, t.fontSize)),
//                            "Unknown font type: " + settings.FontType)
//                        .Then(x => (x.Item1, x.Item2,
//                            layouter.PutNextRectangle(g.MeasureString(x.Item1, x.Item2).ToSize())),
//                            "Layouter thrown error: ")
//                        .Then(x => g.DrawString(x.Item1, x.Item2, settings.FontBrush, x.Item3));
//
//                    if (!result.IsSuccess)
//                        return Result.Fail<Bitmap>(result.Error);
//                    
////                    var font = new Font(settings.FontType, t.fontSize);
////                    var rectangleSize = g.MeasureString(t.word, font).ToSize();
////                    var rect = layouter.PutNextRectangle(rectangleSize);
////                    g.DrawString(t.word, font, settings.FontBrush, rect);
//                }
//            }
//            return mapa;
//        }

        private Result<None> DrawWord(Graphics g, (string word, int fontSize) t)=>
            Result.Of(() => (t.word, new Font(settings.FontType, t.fontSize)),
                "Unknown font type: " + settings.FontType)
            .Then(x => (x.Item1, x.Item2,
                    layouter.PutNextRectangle(g.MeasureString(x.Item1, x.Item2).ToSize())),
                "Layouter thrown error: ")
            .Then(x => g.DrawString(x.Item1, x.Item2, settings.FontBrush, x.Item3));
    }
}