using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Actions
{
    public class AlgorithmSettingsAction : IUiAction
    {
        public string Category => "Алгоритм";
        public string Name => "Настройки...";
        public string Description => "Изменить настройки алгоритма";
        public void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
