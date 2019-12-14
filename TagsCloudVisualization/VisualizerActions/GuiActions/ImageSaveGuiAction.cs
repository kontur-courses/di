using System;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class ImageSaveGuiAction : ImageSaveAction, IGuiAction
    {
        public ImageSaveGuiAction(AppSettings appSettings) : base(appSettings)
        {}

        public override string GetActionDescription()
        {
            return "Сохранить изображение в файл";
        }

        public override string GetActionName()
        {
            return "Сохранить...";
        }

        protected override bool TryGetCorrectFileToSave(out string filepath)
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Environment.CurrentDirectory,
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"
            };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                filepath = dialog.FileName;
                return true;
            }
            filepath = null;
            return false;
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.File;
        }
    }
}