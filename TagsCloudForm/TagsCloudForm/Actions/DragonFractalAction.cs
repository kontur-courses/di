using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public class DragonFractalAction : IUiAction
    {
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            //var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            //SettingsForm.For(dragonSettings).ShowDialog();
            SettingsForm.For(new DragonSettings()).ShowDialog();
            // создаём painter с такими настройками
        }
    }
}
