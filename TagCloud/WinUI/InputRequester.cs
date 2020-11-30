using System;
using TagsCloudVisualisation.Configuration;

namespace WinUI
{
    public class InputRequester<T> : IConfigEntry<T>
    {
        public T GetValue(string description)
        {
            var input = RequestInputForm.RequestInput(description);
            if (string.IsNullOrEmpty(input))
                throw new InvalidOperationException($"Requested: [{description}], got null or empty string");
            var converted = Convert.ChangeType(input, typeof(T));
            return (T) converted;
        }
    }
}