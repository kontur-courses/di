﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class SpeechPart : ICheckable
    {
        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public SpeechPartEnum Value { get; private set; }

        public SpeechPart(string name, SpeechPartEnum value)
        {
            IsChecked = true;
            Name = name;
            Value = value;
        }
    }
}
