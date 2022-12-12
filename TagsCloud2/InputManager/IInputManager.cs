using System.Drawing;

namespace TagsCloud2.InputManager;

public interface IInputManager
{
    public string Path();
    public string PathToSave();
    public int Size();
    public Brush BrushColor();
    public string FontFamilyName();
    public string FormatToSave();
    public bool IsVerticalWords();
    public void GatherInformation();
    public string PathToExcludingWords();
}