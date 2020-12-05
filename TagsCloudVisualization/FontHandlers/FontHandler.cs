﻿using System;
using System.Drawing;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.FontHandlers
{
    public class FontHandler
    {
        private const float FontScale = 1.08f;
        
        public static Font CalculateFont(int frequency, FontSettings fontSettings)
        {
            var newFontSize = GetNewFontSize(frequency, fontSettings);
            return new Font(fontSettings.Font.FontFamily, newFontSize);
        }
        
        private static float GetNewFontSize(int frequency, FontSettings fontSettings)
        {
            var newFontSize = (float) Math.Log(frequency, FontScale) + fontSettings.Font.Size / 10;

            if (newFontSize > fontSettings.MaxSize)
                newFontSize = fontSettings.MaxSize;
            else if (newFontSize < fontSettings.MinSize)
                newFontSize = fontSettings.MinSize;
            
            return newFontSize;
        }
    }
}