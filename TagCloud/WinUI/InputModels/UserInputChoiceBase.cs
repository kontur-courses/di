using System;
using System.Collections.Generic;
using System.Linq;

namespace WinUI.InputModels
{
    public abstract class UserInputChoiceBase<T>
    {
        protected readonly Dictionary<string, UserInputSelectorItem<T>> ItemsByNames;
        
        public UserInputChoiceBase(string description, IEnumerable<UserInputSelectorItem<T>> available)
        {
            ItemsByNames = available.ToDictionary(x => x.Name);
            Description = description;
        }

        public string Description { get; }
        public UserInputSelectorItem<T>[] Available => ItemsByNames.Select(kvp => kvp.Value).ToArray();
        public event Action<UserInputSelectorItem<T>>? AfterSelectionChanged;
        
        protected virtual void OnAfterSelectionChanged(UserInputSelectorItem<T> changed) => 
            AfterSelectionChanged?.Invoke(changed);
    }
}