using System;
using System.Drawing;
using WinUI.InputModels;

namespace WinUI
{
    public interface IUi
    {
        UiLockingOperation StartLockingOperation();
        void AddUserInput<T>(UserInputOneOptionChoice<T> inputModel);
        void AddUserInput<T>(UserInputMultipleOptionsChoice<T> inputModel);
        void AddUserInput(UserInputField fieldInput);
        void AddUserInput(UserInputSizeField sizeInput);
        void AddUserInput(UserInputColor colorInput);
        void OnAfterWordDrawn(Image? newImage, Color backgroundColor);

        event Action? ExecutionRequested;
    }
}