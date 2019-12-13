![Example](https://github.com/TAHK518/di/blob/master/TagsCloud/TagsCloudConsoleClient/result.png)

Аргументы запуска: 
  -w, --width                   (Default: 1920) Width result image.

  -h, --height                  (Default: 1080) Height result image.

  -b, --background              (Default: White) Background color.

  -f, --font                    (Default: Comic Sans MS) Font name.

  -s, --spliter                 (Default: WhiteSpace) Split by line or white space. (Line || WhiteSpace)

  -b, --boringWords             (Default: ) Path to file with boring words. Words must be separated by line.

  -i, --ignoredPartsOfSpeech    (Default: ADVPRO,APRO,CONJ,INTJ,PR,PART,SPRO) Parts of speech to be excluded. Possible
                                parts: A, ADV, ADVPRO, ANUM, APRO, COM, CONJ, INTJ, NUM, PART, PR, S, SPRO, V.

  -g, --GenerationAlgorithm     (Default: CircularCloud) Which algorithm will be used to generate the cloud.
                                (CircularCloud || MiddleCloud)

  -c, --ColorScheme             (Default: RandomColor) Color scheme of result cloud.(RandomColor || RedGreenBlueScheme)

  --help                        Display this help screen.

  --version                     Display version information.

  value pos. 0                  Required. Input files with words.

  value pos. 1                  Required. Result file.
