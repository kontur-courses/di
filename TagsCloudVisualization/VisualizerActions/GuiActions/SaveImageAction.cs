using System;
using System.IO;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class SaveImageAction : IGuiAction
    {
        private readonly AppSettings appSettings;

        public SaveImageAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GetActionDescription()
        {
            return "Сохранить изображение в файл";
        }

        public string GetActionName()
        {
            return "Сохранить...";
        }

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Environment.CurrentDirectory,
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                appSettings.ImageHolder.SaveImage(dialog.FileName);
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.File;
        }
    }
}