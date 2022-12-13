using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.GUI
{
    public class Options : INotifyPropertyChanged
    {
        private string inputWordFilename;
        public string InputWordFilename 
        {
            get => inputWordFilename;
            set
            {
                if (value == inputWordFilename) return;
                inputWordFilename = value;
                OnPropertyChanged(nameof(InputWordFilename));
            }
        }

        private string outputWordFilename;
        public string OutputImageFilename
        {
            get => outputWordFilename;
            set
            {
                if (value == outputWordFilename) return;
                outputWordFilename = value;
                OnPropertyChanged(nameof(OutputImageFilename));
            }
        }

        private int outputImageWidth = 500;
        public int OutputImageWidth
        {
            get => outputImageWidth;
            set
            {
                if(value == outputImageWidth) return;
                outputImageWidth = value;
                OnPropertyChanged(nameof(OutputImageWidth));
            }
        }

        private int outputImageHeight = 500;
        public int OutputImageHeight
        {
            get => outputImageHeight;
            set
            {
                if(value == outputImageHeight) return;
                outputImageHeight = value;
                OnPropertyChanged(nameof(OutputImageHeight));
            }
        }

        private string fontFamily = "Consolas";
        public string FontFamily
        {
            get => fontFamily;
            set
            {
                if(value == fontFamily) return;
                fontFamily = value;
                OnPropertyChanged(nameof(FontFamily));
            }
        }

        private string minFrequencyColorString = "#FFFFAA00";
        public string MinFrequencyColorString
        {
            get => minFrequencyColorString;
            set
            {
                if(value == minFrequencyColorString) return;
                minFrequencyColorString = value;
                OnPropertyChanged(nameof(MinFrequencyColorString));
            }
        }

        private string maxFrequencyColorString = "#FFFF0000";
        public string MaxFrequencyColorString
        {
            get => maxFrequencyColorString;
            set
            {
                if(value == maxFrequencyColorString) return;
                maxFrequencyColorString = value;
                OnPropertyChanged(nameof(MaxFrequencyColorString));
            }
        }

        private float minFrequncyFontSize = 14F;
        public float MinFrequncyFontSize
        {
            get => minFrequncyFontSize;
            set 
            {
                if(value == minFrequncyFontSize) return;
                minFrequncyFontSize = value;
                OnPropertyChanged(nameof(MinFrequncyFontSize));
            }
        }

        private float maxFrequncyFontSize = 24F;
        public float MaxFrequncyFontSize
        {
            get => maxFrequncyFontSize;
            set
            {
                if(value != maxFrequncyFontSize) return;
                maxFrequncyFontSize = value;
                OnPropertyChanged(nameof(MaxFrequncyFontSize));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}