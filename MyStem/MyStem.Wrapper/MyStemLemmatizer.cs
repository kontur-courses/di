using System;
using MyStem.Wrapper.Impl;

namespace MyStem.Wrapper
{
    public class MyStemLemmatizer
    {
        private readonly IMyStem myStem;

        public MyStemLemmatizer(IMyStemBuilder myStemBuilder)
        {
            myStem = myStemBuilder.Create(MyStemOptions.WithoutOriginalForm, MyStemOptions.WithContextualDeHomonymy);
        }

        public string[] GetWords(string text)
        {
            var output = myStem.GetResponse(text);
            if (string.IsNullOrWhiteSpace(output))
                return new string[0];
            return output.Substring(1, output.Length - 2)
                .Split(new[] {"}{"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}