# Tag Cloud

##
Консольное приложение, создающее облако тегов

Аргументы консольной строки можно посмотреть с помощью команды --help

## Примеры раскраски облака тегов

![RandomExample](https://github.com/CaptainBelyash/di/blob/master/TagCloud/random_color_example.png)

![TwoColorExample](https://github.com/CaptainBelyash/di/blob/master/TagCloud/two_color_example.png)

## Примеры облаков тегов, построенных по определенным частям речи

###Все части речи (за исключением скучных) попали в облако

--file c:/shpora/cake.txt --ex jpg --outname cake_all_words --minfont 30 --maxfont 40 --colors "48 213 200"

![AllWords](https://github.com/CaptainBelyash/di/blob/master/TagCloud/cake_all_words.jpg)

###Только существительные

--file c:/shpora/cake.txt --ex jpg --outname cake_nouns --minfont 30 --maxfont 40 --colors "48 213 200" --gramparts Noun

![NounWords](https://github.com/CaptainBelyash/di/blob/master/TagCloud/cake_nouns.jpg)

###Cуществительные и глаголы

--file c:/shpora/cake.txt --ex jpg --outname cake_nouns_verbs --minfont 30 --maxfont 40 --colors "48 213 200" --gramparts Noun,Verb

![NounVerbsWords](https://github.com/CaptainBelyash/di/blob/master/TagCloud/cake_nouns_verbs.jpg)


## Шутка длиною в титулы последнего императора (сделано черновиком, не разделяющим строки по пробелам)

![Nikolai](https://github.com/CaptainBelyash/di/blob/master/TagCloud/nikolai.png)
