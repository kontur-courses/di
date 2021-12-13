using System;
using System.IO;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.UI
{
    public class ChangePreprocessorStateAction : ConsoleUiAction
    {
        public override string Category => "Preprocessors";
        public override string Name => "ChangePreprocessorState";
        public override string Description { get; }

        public ChangePreprocessorStateAction(TextReader reader, TextWriter writer)
            : base(reader, writer)
        {
        }

        public override void Perform()
        {
            writer.WriteLine("Enter Preprocessor name");
            while (true)
            {
                var processorName = reader.ReadLine();
               
                var userPreprocessor = PreprocessorsRegistrator.GetActivePreprocessors()
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