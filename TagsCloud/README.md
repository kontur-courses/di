# Tag Cloud Generator 2.0

## Что реализовано на данный момент

#### Обязательный функционал

- [x] .txt файл со словами по одному в строке обрабатывается
- [x] Приведение к нижнему регистру и исключение слов
- [x] Задание цвета, шрифта и размера изображения.
- [x] Генерация разных облаков на основе одного текста (при помощи разных алгоритмов перемешивания токенов, разных расцветок, разных калькуляторов размера тега)
- [x] Консольный клиент
- [x] Autofac container

#### Дополнительный функционал (по заданию)

- [x] Кастомный список скучных слов (при помощи .txt файла со словами по одному в строке)
- [x] Ввод из литературного текста (реализовано разбиение текста на слова, НЕ реализовано приведение слов в нач. форму)
- [x] Поддержка разных форматов изображений (Png, Jpeg, Wmf, легко расширить до всех, поддерживаемых ImageFormat)
- [x] Поддержка разных алгоритмов расцветки слов (расцветка в зависимости от количества вхождений слова, рандомная расцветка)
- [x] Поддержка разных форматов текста (.txt, .pdf, .docx)

#### Доп. функционал (не было в задании)

- [x] Перемешивание токенов (сортировка по возрастанию или убыванию, рандомное перемешивание)
- [x] Кастомный калькулятор размера токена (позволяет указать свою зависимость между количеством вхождений токена в тексте и его размером в облаке тегов)
- [x] Возможность приводить слова к разному виду (к верхнему регистру, к нижнему регистру, обрезать пробелы и т.д.)

## Консольное приложение

>[Папка с примерами команд и результами генерации](https://github.com/Sc222/di/tree/hometask/TagsCloud/Examples/Console%20app)


## Примеры генерации облаков тегов

#### Tag Cloud, сгенерированный из текста песни

![](https://raw.githubusercontent.com/Sc222/di/hometask/TagsCloud/Examples/Txt/result.png)
>[Папка с примером](https://github.com/Sc222/di/tree/hometask/TagsCloud/Examples/Txt)

##### Исходные данные

- [Входной текст (**txt**)](https://github.com/Sc222/di/blob/hometask/TagsCloud/Examples/Txt/song.txt)
- [Исключенные слова](https://github.com/Sc222/di/blob/hometask/TagsCloud/Examples/Txt/exclude.txt)


##### Некоторые параметры генерации

- Тема  - [`GrayDarkTheme`](https://github.com/Sc222/di/blob/hometask/TagsCloud/TagsCloudVisualization/Styling/Themes/GrayDarkTheme.cs) 
- Перемешиватель тегов - [`TokenShufflerRandom`](https://github.com/Sc222/di/blob/hometask/TagsCloud/TagsCloudTextProcessing/Shufflers/TokenShufflerRandom.cs) с параметром 10
- Шрифт - **Arial Black**

#### Tag Cloud, сгенерированный из фрагмента документации к игровому движку Godot

![](https://raw.githubusercontent.com/Sc222/di/hometask/TagsCloud/Examples/Pdf/result.png)

>[Папка с примером](https://github.com/Sc222/di/tree/hometask/TagsCloud/Examples/Pdf)

##### Исходные данные

- [Входной текст (**pdf**)](https://github.com/Sc222/di/blob/hometask/TagsCloud/Examples/Pdf/godot.pdf)
- [Исключенные слова](https://github.com/Sc222/di/blob/hometask/TagsCloud/Examples/Pdf/exclude.txt)
##### Некоторые параметры генерации

- Тема  - [`GodotEngineTheme`](https://github.com/Sc222/di/blob/hometask/TagsCloud/TagsCloudVisualization/Styling/Themes/GodotEngineTheme.cs) 
- Перемешиватель тегов - [`TokenShufflerDescending`](https://github.com/Sc222/di/blob/hometask/TagsCloud/TagsCloudTextProcessing/Shufflers/TokenShufflerDescending.cs)
- Шрифт - **Berlin Sans**

#### Tag Cloud, сгенерированный из статьи про Pixel Art

![](https://raw.githubusercontent.com/Sc222/di/hometask/TagsCloud/Examples/Docx/result.png)

>[Папка с примером](https://github.com/Sc222/di/tree/hometask/TagsCloud/Examples/Docx)

##### Исходные данные

- [Входной текст (**docx**)](https://github.com/Sc222/di/blob/hometask/TagsCloud/Examples/Docx/pixel%20art.docx)
- [Исключенные слова](https://github.com/Sc222/di/blob/hometask/TagsCloud/Examples/Docx/exclude.txt)

##### Некоторые параметры генерации

- Тема  - [`PixelArtTheme`](https://github.com/Sc222/di/blob/hometask/TagsCloud/TagsCloudVisualization/Styling/Themes/PixelArtTheme.cs) 
- Перемешиватель тегов - [`TokenShufflerAscending`](https://github.com/Sc222/di/blob/hometask/TagsCloud/TagsCloudTextProcessing/Shufflers/TokenShufflerAscending.cs)
- Шрифт - **Bauhaus 96**
