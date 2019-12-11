Запускать нужно проект TagsCloudContainer.

Теперь по умолчанию запускается GUI, чтобы снова перейти на консоль нужно:
в классе `Program` в конфигурировании контейнера зарегистрировать
`ConsoleUserInterface` как `IUserInterface` и как `IResultDisplay`.

Примеры запуска:

`TagsCloudContainer.exe -i yourtext.txt`

`TagsCloudContainer.exe -i yourtext.txt -o yourpng.png -w 1280 -h 720 -f Arial -e Bmp --colors Red Blue Green`

Подробнее об аргументах программы, их значениях по умолчанию, какие из них обязательны, можно узнать запустив:

`TagsCloudContainer.exe --help`

Тестовые файлики для запусков лежат в корневом каталоге проекта и имеют имена:
`smallTestFile.txt`, `averageTestFile.txt`, `bigTestFile.txt`, `arbitraryTestFile.txt`

`arbitraryTestFile.txt` служит для проверки того, что программа умеет работать
с произвольным txt файлом.