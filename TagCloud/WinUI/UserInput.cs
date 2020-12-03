using System.Collections.Generic;
using System.Linq;
using WinUI.InputModels;

namespace WinUI
{
    public static class UserInput
    {
        public static UserInputBoolean Boolean(string description, bool initialValue = false) =>
            new UserInputBoolean(description, initialValue);

        public static UserInputField Field(string description) => new UserInputField(description);

        public static UserInputMultipleChoice<TService> MultipleChoice<TService>(
            IDictionary<string, TService> source,
            string description) =>
            new UserInputMultipleChoice<TService>(description, SelectorItems(source));

        public static UserInputSingleChoice<TService> SingleChoice<TService>(
            IDictionary<string, TService> source,
            string description) =>
            new UserInputSingleChoice<TService>(description, SelectorItems(source));

        private static UserInputSelectorItem<T>[] SelectorItems<T>(IDictionary<string, T> source) =>
            source.Select(x => new UserInputSelectorItem<T>(x.Key, x.Value))
                .OrderBy(x => x.Name)
                .ToArray();
    }
}