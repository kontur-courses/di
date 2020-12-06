using System;
using System.Linq;

namespace MyStem.Wrapper.Impl
{
    public sealed class MyStemBuilder : IMyStemBuilder
    {
        private readonly string path;

        public MyStemBuilder(string path)
        {
            this.path = path;
        }

        public IMyStem Create(params MyStemOptions[] args)
        {
            var launchArgs = string.Join(" ", args.Select(OptionToExecutionArg));
            return new MyStem(path, launchArgs);
        }

        private static string OptionToExecutionArg(MyStemOptions option) => option switch
        {
            MyStemOptions.LinearMode => "-n",
            MyStemOptions.CopyEverything => "-c",
            MyStemOptions.OnlyLexical => "-w",
            MyStemOptions.WithoutOriginalForm => "-l",
            MyStemOptions.WithGrammarInfo => "-i",
            MyStemOptions.JoinSingleLemmaWordForms => "-g",
            MyStemOptions.PrintEndOfSentenceMarker => "-s",
            MyStemOptions.WithContextualDeHomonymy => "-d",
            _ => throw new ArgumentOutOfRangeException(nameof(option), $"Unsupported {nameof(MyStemOptions)} {option}")
        };
    }
}