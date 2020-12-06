using System;
using MyStem.Wrapper.Impl;

namespace MyStem.Wrapper
{
    public class Lemmatizer
    {
        private readonly IMyStem myStem;

        public Lemmatizer(IMyStemBuilder myStemBuilder)
        {
            myStem = myStemBuilder.Create(MyStemOutputFormat.Text,
                MyStemOptions.WithoutOriginalForm, MyStemOptions.WithContextualDeHomonymy);
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