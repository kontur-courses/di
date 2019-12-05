using System;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class TextFileAction : IGuiAction
    {
        private readonly PictureBoxImageHolder imageHolder;

        public TextFileAction(PictureBoxImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public string GetActionDescription()
        {
            return "Открыть файл с текстом";
        }

        public string GetActionName()
        {
            return "Текст...";
        }

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "txt",
                InitialDirectory = Environment.CurrentDirectory
            };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var newImage = TagCloudVisualizer.GetTagCloudFromFile(dialog.FileName);
                imageHolder.SetImage(newImage);
            }
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.File;
        }
    }
}