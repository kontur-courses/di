using System;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class TextFileAction : IGuiAction
    {
        private readonly AppSettings appSettings;

        public TextFileAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
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
                appSettings.CurrentFile = dialog.FileName;
                var newImage = appSettings.CurrentInterface.GetTagCloud();
                appSettings.ImageHolder.SetImage(newImage);
            }
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.File;
        }
    }
}