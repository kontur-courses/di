using System;
using System.IO;
using System.Linq;
using TagsCloudContainer.Common;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.UI
{
    public class ChangePreprocessorStateAction : IUiAction
    {
        private readonly TextWriter writer;
        private readonly TextReader reader;
        public string Category => "Preprocessors";
        public string Name => "ChangePreprocessorState";
        public string Description { get; }

        public ChangePreprocessorStateAction(TextWriter writer, TextReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        public void Perform()
        {
            writer.WriteLine("Enter Preprocessor name");
            while (true)
            {
                var processorName = reader.ReadLine();

                var userPreprocessor = AppDomain.CurrentDomain.GetAssemblies()
                    .First(a => a.FullName.Contains("TagsCloudContainer"))
                    .GetTypes()
                    .Where(t => t.IsInstanceOf<IPreprocessor>())
                    .FirstOrDefault(t => t.Name == processorName);

                if (userPreprocessor != null)
                {
                    ChangeState(userPreprocessor);
                    return;
                }
                writer.WriteLine("Preprocessor with that name not found, " +
                                 "check existing preprocessors by command GetAllPreprocessors");
            }
        }

        private void ChangeState(Type preprocessorType)
        {
            var prop = preprocessorType.GetProperty(nameof(State));
            var state = (State)prop.GetValue(null);
            state = state == State.Active ? 
                State.Inactive : State.Active;
            prop.SetValue(null, state);
        }
    }
}