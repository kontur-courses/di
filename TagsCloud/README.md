# Tag cloud
## Available Parameters:
 __-f__, __--file__       Required. File with words to build tag cloud from.

 __-w__, __--width__      (Default: 1024) Defines width of the picture.

 __-h__, __--height__     (Default: 1024) Defines width of the picture.

 __--font__           (Default: Arial) Defines font of words.

 __--bgcolor__        (Default: LightGray) Defines color of the background.

 __--colorScheme__    (Default: Red) Defines color scheme of the font.

 __--boring__         Defines boring parts of speech that will not be listed in
                  result. Possible parts are: Adjective, Adverb,
                  PronominalAdverb, NumeralAdjective, PronounAdjective,
                  CompositePart, Conjunction, Interjection, Numeral, Particle,
                  Pretext, Noun, PronounNoun, Verb

 __--only__           Defines boring parts of speech that will not be listed in
                  result. Possible parts are: Adjective, Adverb,
                  PronominalAdverb, NumeralAdjective, PronounAdjective,
                  CompositePart, Conjunction, Interjection, Numeral, Particle,
                  Pretext, Noun, PronounNoun, Verb

 __--infinitive__     (Default: false) Uses infinitive form of words.

 __--dangle__         (Default: 0,196349540849362) Defines dangle in spiral cloud.

 __--minFontSize__    (Default: 10) Defines minimum font size.

 __--maxFontSize__    (Default: 100) Defines maximum font size.

 __--sizeDefiner__    (Default: Frequency) Defines type of the size
                  definer.Possible types are: Random, Frequency

 __--help__           Display this help screen.

 __--version__        Display version information.

## Example
TagCloud.exe -f C:\Users\Дима\Desktop\TestFile.txt --infinitive  --boring PronounNoun --bgcolor LightGray --colorScheme Random
![Image alt](https://github.com/DimaIvanovskiy/di/blob/feature/TagsCloud/TestFile.jpg)
