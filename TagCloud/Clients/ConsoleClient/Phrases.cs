namespace TagCloud.Clients.ConsoleClient;

public class Phrases
{
    public static string GetArrow(int indent) => new string(' ', indent) + "> ";
    public static readonly string TryAgain = "Повторить попытку? [y/n]: ";
    public static readonly string Yes = "y";

    public static readonly string Hello = "Вас приветствует создатель облака тегов." + Environment.NewLine +
                                          "Для моей работы нужен файл со словами для облака, которые записаны в столбик." +
                                          Environment.NewLine +
                                          "Так же Вы можете указать список скучных по вашему мнению слов, " +
                                          "которые нужно исключить из построения облака. Их так же нужно указать в файле " +
                                          "по одномму слову в каждой стрроке.";
    
    public static readonly string GoodBy =
        Environment.NewLine +
        "Спасибо за использование этой программы" +
        Environment.NewLine + "До свидания!";
    
    public static readonly string AskingFullPathToOutImage =
        Environment.NewLine +
        "Пожалуйста, введите полный путь к файлу, в который необходимо сохранить изображение: ";

    public static readonly string AskingFullPathToText =
        Environment.NewLine +
        "Пожалуйста, введите полный путь к вашему файлу со словами";
    public static readonly string FailGettingFullPath = "    Мне не удалось обнаружить указанный файл. ";

    public static readonly string AskingFullPathToBoringWords =
        Environment.NewLine +
        "Пожалуйста, введите полный путь к вашему файлу со словами, коорые соедует исключить при формировании облака.";

    public static readonly string SuccessUploadBoringWords = "Список скучных слов загружен успешно";

    public static readonly string AskingImageSize =
        Environment.NewLine +
        "Пожалуйста, введите размеры изображения, которое хотите получить Ш*В (в пикселях)" +
        Environment.NewLine +
        "Например 800*500: ";
    public static readonly string FailGettingImageSize = "    Неверный формат записи размеров. ";

    public static readonly string AskingBgColor =
        Environment.NewLine +
        "Пожалуйста, введите цвет фона на английском. Например white: ";
    public static readonly string FailGettingColor = "    Заданный цвет не определён. ";
    public static readonly string AskingWordColor = "    Цвет слов на английском. Например black: ";

    public static readonly string FailGettingFont = "    Неверно введено название шрифта. ";
    public static readonly string FailGettingIntValue = "    Неверно введено число. Введите целое число. ";
    public static readonly string FailGettingDoubleValue =
        "    Неверно введено число. Введите десятичное число через запятую. ";

    public static readonly string StartCreatingNewWordVisualisation =
        Environment.NewLine +
        new string('-', 10) + 
        Environment.NewLine +
        "Создание нового правила оформления слов";

    public static readonly string AskingWordImportance =
        "    Значение важности слова в тексте, с корого начинается данное оформление." +
        Environment.NewLine +
        "    Дробное число через запятую от 0 до 1 ( 0 - совсем не важно, 1 - самое важное): ";

    public static readonly string AskingFontSize = "    Размер шрифта: ";
    public static readonly string AskingFontName = "    Название стиля шрфта на английском: ";

    public static readonly string EndCreatingNewWordVisualisation = new string('-', 10);

    public static readonly string AskingAddingWordVisualisationRule =
        Environment.NewLine +
        "Хотите задать настройки для рисования слов? [y/n]: ";

    public static readonly string AskingAddingUsersBoringWords =
        Environment.NewLine +
        "Хотите указать файл со скучными словами, которые следует исключить при формировании облака? [y/n]: ";

    public static readonly string FailGettingFileEncoder =
        "Не удалось обнаружить подходящий обработчик файла данных. " +
        Environment.NewLine +
        "Попробуйте сменить тип файла и запустить программу заново.";

    public static readonly string SuccessSaveImage = Environment.NewLine + "Файл сохранён успешно в ";

    public static readonly string FailImageSaving =
        Environment.NewLine +
        "Произошла ошибка при сохранении изображения. Запустите программу ещё раз";

    public static string UnexpectedError(Exception e) => "Произошла непредвиденная ошибка " + Environment.NewLine +
                                                         e.Message + Environment.NewLine +
                                                         "Попробуйте перезапустить программу";
}