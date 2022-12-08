using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Visualisator;

namespace TagsCloudContainer.Actions
{
    public class DrawImageAction : IUiAction
    {
        private IPainter painter;

        public DrawImageAction(IPainter painter)
        {
            this.painter = painter;
        }
        public string Category => "Изображение";
        public string Name => "Получить картинку";
        public string Description => "Отрисовывает Облако тегов";

        public void Perform()
        {
            painter.Paint();
        }
    }
}
