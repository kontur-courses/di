using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Actions
{
    public class DrawImageAction : IUiAction
    {
        public string Category => "Изображение";
        public string Name => "Получить картинку";
        public string Description => "Отрисовывает Облако тегов";
        public void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
