using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudVisualization.Styling.Themes
{
    public interface ITheme
    {
        //todo ДОДЕЛАТЬ РИДМИ + ДОБАВИТЬ ТУДА ПРИМЕР ИСПОЛЬЗОВАНИЯ КОНС. ПРИЛ.
        //todo сделать Theme ИНТЕРФЕЙСОМ, методы преобразования цвета в brush или в pen вынести
        //todo - массив для rectangles, массив для слов, спец интерфейс для расцветки слов (ИЛИ забить на прямоугольники и работать только со словами)
        //todo тесты тесты тесты (убрать прямоугольники полностью, заменить их  на теги)
        //todo console app USING LIB
        //todo container setup
        //todo убрать нафиг класс cloud (разбить его) + подумать над классом styling
        string[] TextColors { get; }

        string BackgroundColor { get; }
    }
}