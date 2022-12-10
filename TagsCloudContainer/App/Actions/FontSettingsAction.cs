using System.Drawing;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Actions
{
    public class FontSettingsAction : IUiAction
    {
        private readonly FontText font;

        public FontSettingsAction(FontText font)
        {
            this.font = font;
        }

        public string Category => "Настройки";
        public string Name => "Шрифт...";
        public string Description => "Максимальный шрифт текста";

        public void Perform()
        {
            SettingsForm.For(font).ShowDialog();
        }
    }
}