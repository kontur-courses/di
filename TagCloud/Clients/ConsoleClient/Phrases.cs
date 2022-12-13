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

    public static readonly string AskingFontSize =
        "Введите минимальный и максимальный размеры шрифта, которые хотите видеть, через пробел: ";

    public static readonly string FailGettingFontSize = "Не удалось обработать введённые числа. ";

    public static readonly string AskingWordsColors =
        Environment.NewLine +
        "Пожалуйста, введите цвета, в которые необходимо раскрасить слова. Вводите цвета " +
        "от самого редко используемого, к самому часто встречающемуся."
        + Environment.NewLine +
        "Например white-gray-black: ";

    public static readonly string FailGettingWordsColors = "Не удалось прочитать цвета. ";
    
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