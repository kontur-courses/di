# Tag cloud
## Available Parameters:
 -f, --file       Required. File with words to build tag cloud from.

 -w, --width      (Default: 1024) Defines width of the picture.

 -h, --height     (Default: 1024) Defines width of the picture.

 --font           (Default: Arial) Defines font of words.

 --bgcolor        (Default: LightGray) Defines color of the background.

 --colorScheme    (Default: Red) Defines color scheme of the font.

 --boring         Defines boring parts of speech that will not be listed in
                  result. Possible parts are: Adjective, Adverb,
                  PronominalAdverb, NumeralAdjective, PronounAdjective,
                  CompositePart, Conjunction, Interjection, Numeral, Particle,
                  Pretext, Noun, PronounNoun, Verb

 --only           Defines boring parts of speech that will not be listed in
                  result. Possible parts are: Adjective, Adverb,
                  PronominalAdverb, NumeralAdjective, PronounAdjective,
                  CompositePart, Conjunction, Interjection, Numeral, Particle,
                  Pretext, Noun, PronounNoun, Verb

 --infinitive     (Default: false) Uses infinitive form of words.

 --dangle         (Default: 0,196349540849362) Defines dangle in spiral cloud.

 --minFontSize    (Default: 10) Defines minimum font size.

 --maxFontSize    (Default: 100) Defines maximum font size.

 --sizeDefiner    (Default: Frequency) Defines type of the size
                  definer.Possible types are: Random, Frequency

 --help           Display this help screen.

 --version        Display version information.

Exampple:
TagCloud.exe -f C:\Users\Дима\Desktop\TestFile.txt --infinitive  --boring PronounNoun --bgcolor LightGray --colorScheme Random
![Image alt](https://github.com/DimaIvanovskiy/di/blob/feature/TagsCloud/TestFile.jpg)
