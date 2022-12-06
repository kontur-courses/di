using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Actions
{
    public class SaveImageAction : IUiAction
    {
        public string Category => "Файл";
        public string Name => "Сохранить как...";
        public string Description => "Сохранить файл как";

        public void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
