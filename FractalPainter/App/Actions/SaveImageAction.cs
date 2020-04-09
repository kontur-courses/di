﻿using System.IO;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageDirectoryProvider imageDirectoryProvider;
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IImageHolder imageHolder, IImageDirectoryProvider imageDirectoryProvider)
        {
            this.imageHolder = imageHolder;
            this.imageDirectoryProvider = imageDirectoryProvider;
        }

        #region IUiAction

        public string Category => "Файл";
        public int Order => 0;
        public int CategoryOrder => 0;
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp"
            };
            
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                imageHolder.SaveImage(dialog.FileName);
            }
        }

        #endregion
    }
}