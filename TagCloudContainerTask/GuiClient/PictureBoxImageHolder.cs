﻿using System;
using System.Drawing;
using System.Windows.Forms;
using App.Infrastructure;
using App.Infrastructure.SettingsHolders;

namespace GuiClient
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly Lazy<MainForm> mainForm;
        private readonly IOutputResultSettingsHolder outputResultSettings;
        private readonly IImageSizeSettingsHolder sizeSettings;

        public PictureBoxImageHolder(
            IImageSizeSettingsHolder sizeSettings,
            IOutputResultSettingsHolder outputResultSettings,
            ICloudGenerator cloudGenerator,
            Lazy<MainForm> mainForm
        )
        {
            this.sizeSettings = sizeSettings;
            this.outputResultSettings = outputResultSettings;
            this.mainForm = mainForm;
            this.cloudGenerator = cloudGenerator;
        }

        public void GenerateImage()
        {
            Image = cloudGenerator.GenerateCloud();
        }

        public Graphics StartDrawing()
        {
            FailIfNotInitialized();
            return Graphics.FromImage(Image);
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage()
        {
            if (Image != null)
                mainForm.Value.ClientSize = sizeSettings.Size;

            Image = cloudGenerator.GenerateCloud();
        }

        public void SaveImage()
        {
            FailIfNotInitialized();
            Image.Save(outputResultSettings.OutputFilePath, outputResultSettings.ImageFormat);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
        }
    }
}