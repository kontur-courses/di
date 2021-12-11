using System;

namespace TagsCloudContainer.FileReader
{
    [Flags]
    public enum TextFileFormat
    {
        Txt = 0x0,
        Json,
        Csv,
        Xml,
        Doc,
        Docx,
        Rtf
    }
}