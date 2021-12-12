using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TagsCloudContainer.Common;
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
            var preprocessor = typeof(IPreprocessor);
            while (true)
            {
                var processorName = reader.ReadLine();

                var userPreprocessor = AppDomain.CurrentDomain.GetAssemblies()
                    .First(a => a.FullName.Contains("TagsCloudContainer"))
                    .GetTypes()
                    .Where(t => preprocessor.IsAssignableFrom(t))
                    .FirstOrDefault(t => t.Name == processorName);

                if (userPreprocessor != null)
                {
                    ChangeState(userPreprocessor.GetCustomAttribute<StateAttribute>());
                    return;
                }
                writer.WriteLine("Preprocessor with that name not found, " +
                                 "check existing preprocessors by command GetAllPreprocessors");
            }
        }

        private void ChangeState(StateAttribute state)
        {
            state.State = state.State == State.Active ? 
                State.Inactive : State.Active;
        }
    }
}