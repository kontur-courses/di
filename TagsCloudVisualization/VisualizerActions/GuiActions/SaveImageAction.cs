using System;
using System.IO;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class SaveImageAction : IGuiAction
    {
        private readonly PictureBoxImageHolder imageHolder;

        public SaveImageAction(PictureBoxImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
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
                imageHolder.SaveImage(dialog.FileName);
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.File;
        }
    }
}