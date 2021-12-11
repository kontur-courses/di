using System;
using System.IO;
using App.Infrastructure.FileInteractions.Readers;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.FileInteractions.Readers
{
    public class ReaderFactory : IReaderFactory
    {
        private readonly IInputFileSettingsHolder inputFileSettings;

        public ReaderFactory(IInputFileSettingsHolder inputFileSettings)
        {
            this.inputFileSettings = inputFileSettings;
        }

        public ILinesReader CreateReader()
        {
            switch (Path.GetExtension(inputFileSettings.InputFileName))
            {
                case ".txt":
                    return new FromStreamReader(new StreamReader(inputFileSettings.InputFileName));
                case ".doc":
                case ".docx":
                    return new FromDocReader(inputFileSettings.InputFileName);
            }

            throw new NotImplementedException("Unknown input file extension");
        }
    }
}