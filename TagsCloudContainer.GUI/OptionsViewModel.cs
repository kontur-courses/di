using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.GUI
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        private static readonly PropertyInfo[] properties = typeof(OptionsViewModel).GetProperties();

        private Options currentOptions;
        public Options CurrentOptions
        {
            get => currentOptions;
            set
            {
                if(value == currentOptions) return;
                currentOptions = value;

                foreach (var property in properties)
                    OnPropertyChanged(property.Name);
            }
        }

        public string InputWordFilename 
        {
            get => currentOptions.InputWordFilename;
            set
            {
                if (value == currentOptions.InputWordFilename) return;
                currentOptions.InputWordFilename = value;
                OnPropertyChanged(nameof(InputWordFilename));
            }
        }

        public string OutputImageFilename
        {
            get => currentOptions.OutputImageFilename;
            set
            {
                if (value == currentOptions.OutputImageFilename) return;
                currentOptions.OutputImageFilename = value;
                OnPropertyChanged(nameof(OutputImageFilename));
            }
        }

        public int OutputImageWidth
        {
            get => currentOptions.OutputImageWidth;
            set
            {
                if(value == currentOptions.OutputImageWidth) return;
                currentOptions.OutputImageWidth = value;
                OnPropertyChanged(nameof(OutputImageWidth));
            }
        }

        public int OutputImageHeight
        {
            get => currentOptions.OutputImageHeight;
            set
            {
                if(value == currentOptions.OutputImageHeight) return;
                currentOptions.OutputImageHeight = value;
                OnPropertyChanged(nameof(OutputImageHeight));
            }
        }

        public string FontFamily
        {
            get => currentOptions.FontFamily;
            set
            {
                if(value == currentOptions.FontFamily) return;
                currentOptions.FontFamily = value;
                OnPropertyChanged(nameof(FontFamily));
            }
        }

        public string MinFrequencyColorString
        {
            get => currentOptions.MinFrequencyColorString;
            set
            {
                if(value == currentOptions.MinFrequencyColorString) return;
                currentOptions.MinFrequencyColorString = value;
                OnPropertyChanged(nameof(MinFrequencyColorString));
            }
        }

        public string MaxFrequencyColorString
        {
            get => currentOptions.MaxFrequencyColorString;
            set
            {
                if(value == currentOptions.MaxFrequencyColorString) return;
                currentOptions.MaxFrequencyColorString = value;
                OnPropertyChanged(nameof(MaxFrequencyColorString));
            }
        }

        public float MinFrequncyFontSize
        {
            get => currentOptions.MinFrequncyFontSize;
            set 
            {
                if(value == currentOptions.MinFrequncyFontSize) return;
                currentOptions.MinFrequncyFontSize = value;
                OnPropertyChanged(nameof(MinFrequncyFontSize));
            }
        }

        public float MaxFrequncyFontSize
        {
            get => currentOptions.MaxFrequncyFontSize;
            set
            {
                if(value != currentOptions.MaxFrequncyFontSize) return;
                currentOptions.MaxFrequncyFontSize = value;
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