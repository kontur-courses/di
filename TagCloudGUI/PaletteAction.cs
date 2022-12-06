using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGraphicalUserInterface
{
     
    public class PaletteAction : IActionForm
    {
        private Palette palette;
        public PaletteAction(Palette palette)
        {
            this.palette = palette;
        }

        public string Category => "Настройки";
        public string Name => "Цвет вывода";
        public string Description => "Цвета для рисования фракталов";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}
