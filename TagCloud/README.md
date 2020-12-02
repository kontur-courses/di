﻿#Примеры запуска

с аргументами по умолчанию
...di\TagCloud\bin\Debug\net48> TagCloud.exe

help
...di\TagCloud\bin\Debug\net48> TagCloud.exe -h
...di\TagCloud\bin\Debug\net48> TagCloud.exe --help
...di\TagCloud\bin\Debug\net48> TagCloud.exe -?

со своими аргументами
...di\TagCloud\bin\Debug\net48> TagCloud.exe -i input.txt -f "Verdana -s 1200,1000 -c random
...di\TagCloud\bin\Debug\net48> TagCloud.exe --input input.txt --font "Verdana --size 1200,1000 -coloring words

__Где:__

путь до input.txt
..\di\input.txt

--size width,height

-c random|words|location 
По умолчанию: location 