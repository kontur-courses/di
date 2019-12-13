using System;
using System.Drawing.Imaging;
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
                Filter = "Изображения (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                switch (dialog.FilterIndex)
                {
                    case 1:
                        appSettings.ImageHolder.SaveImage(dialog.FileName,
                            ImageFormat.Png);
                        break;

                    case 2:
                        appSettings.ImageHolder.SaveImage(dialog.FileName,
                            ImageFormat.Jpeg);
                        break;

                    case 3:
                        appSettings.ImageHolder.SaveImage(dialog.FileName,
                            ImageFormat.Bmp);
                        break;
                }
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.File;
        }
    }
}