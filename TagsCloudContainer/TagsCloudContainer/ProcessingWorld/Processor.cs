using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using TagsCloudContainer.CloudDrawing;
using TagsCloudContainer.PreprocessingWorld;
using TagsCloudContainer.Reader;

namespace TagsCloudContainer.ProcessingWorld
{
    public class Processor : IProcessor
    {
        private IReader reader;
        private IPreprocessingWorld preprocessingWorld;
        private ICircularCloudDrawing circularCloudDrawing;
        public Processor(
            IReader reader,
            IPreprocessingWorld PreprocessingWorld,
            ICircularCloudDrawing CircularCloudDrawing)
        {
            this.reader = reader;
            preprocessingWorld = PreprocessingWorld;
            circularCloudDrawing = CircularCloudDrawing;
            circularCloudDrawing.SetBackground(Color.Aquamarine);
        }

        public void Run(string pathToFile, string pathSave)
        {
            var dict = new Dictionary<string, int>();
            foreach (var str in preprocessingWorld.Preprocessing(reader.GetWorldSet(pathToFile))) 
                dict[str] = dict.ContainsKey(str) ? dict[str] + 1: 1;
            foreach (var pair in dict)
            {
                circularCloudDrawing.DrawString(pair.Key,
                    new Font("Arial", pair.Value + 5),
                    Brushes.Black,
                    new StringFormat() {LineAlignment = StringAlignment.Center} );
            }
            circularCloudDrawing.SaveImage(pathSave);

        }
    }
}