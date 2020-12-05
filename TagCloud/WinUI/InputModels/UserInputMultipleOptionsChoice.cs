﻿using System.Collections.Generic;
using System.Linq;

namespace WinUI.InputModels
{
    public class UserInputMultipleOptionsChoice<T> : UserInputChoiceBase<T>
    {
        private readonly Dictionary<string, bool> selectionByNames;

        public UserInputMultipleOptionsChoice(string description, UserInputSelectorItem<T>[] available) 
            : base(description, available)
        {
            selectionByNames = Available.ToDictionary(x => x.Name, _ => false);
        }

        public IEnumerable<UserInputSelectorItem<T>> Selected => Available.Where(x => IsSelected(x.Name));
        public bool IsSelected(string name) => selectionByNames[name];

        public void ChangeItemSelection(string name) => selectionByNames[name] = !IsSelected(name);

        public void SetSelection(string name, bool isSelected)
        {
            if (IsSelected(name) != isSelected)
                ChangeItemSelection(name);
        }
    }
}