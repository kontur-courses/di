using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TagCloud.TextFiltration;
using TagCloud.Visualization;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class SelectBlackListAction : IUiAction
    {
        private readonly IDirectoryProvider directoryProvider;
        private readonly CloudVisualization cloudVisualization;
        private readonly CloudPainter cloudPainter;
        private readonly BlacklistSettings blacklistSettings;
        private readonly BlacklistMaker blacklistMaker;


        public SelectBlackListAction(IDirectoryProvider directoryProvider,
            CloudVisualization cloudVisualization, CloudPainter cloudPainter, BlacklistSettings blacklistSettings,
            BlacklistMaker blacklistMaker)
        {
            this.directoryProvider = directoryProvider;
            this.cloudVisualization = cloudVisualization;
            this.cloudPainter = cloudPainter;
            this.blacklistSettings = blacklistSettings;
            this.blacklistMaker = blacklistMaker;
        }

        public string Category => "Черный список";
        public string Name => "Выбрать";
        public string Description => "Выбрать черный список из файла";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(directoryProvider.Directory),
                DefaultExt = "txt",
                Multiselect = true
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                blacklistSettings.FilesWithBannedWords = new HashSet<string>(dialog.FileNames);
                blacklistMaker.CreateBlackList();
                cloudVisualization.ResetWordsFrequenciesDictionary();
                cloudPainter.Paint();
            }
        }
    }
}