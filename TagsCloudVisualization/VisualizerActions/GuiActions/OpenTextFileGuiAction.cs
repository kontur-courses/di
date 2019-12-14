using System;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class OpenTextFileGuiAction : OpenTextFileAction, IGuiAction
    {
        public OpenTextFileGuiAction(AppSettings appSettings) : base(appSettings)
        {}

        public override string GetActionDescription()
        {
            return "Открыть файл с текстом";
        }

        public override string GetActionName()
        {
            return "Текст...";
        }

        protected override bool TryGetCorrectFileToOpen(out string filepath)
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