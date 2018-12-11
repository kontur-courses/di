#TagCloudUtility 1.0.0.0
#Copyright c  2018

## Input:
Format: 
  -w, --words           Required. Path to words in format .../words.txt (type is required)

  -p, --picture         Required. Path to picture in format .../picture.png (type is required)

  -d, --drawSettings    (Default: WordsInRectangles) Draw settings:(only words == 0)(words in rectangles == 1)(only
                        rectangles == 2)(rectangles with numeration == 3)

  -t, --tags            Path to tags in format .../tags.txt (type is required)

  -b, --brush           (Default: #000000) Color of brush(in html color)

  -c, --color           (Default: #FFFFFF) Color of background(in html color)

  -s, --size            (Default: 1000x1000) Size of picture in format (width)x(height)

  -f, --font            (Default: arial) Font family name

  --stopWords           Path to stop words in format .../stopwords.txt (type is required)

  --help                Display this help screen.

  --version             Display version information.

## Example of using:
Exe in C:.../Test/TagCloud.Utility.exe  
Input: -w words.txt -p result.pnh -t tagGroups.txt -b red -f arial
In result  
Words should be in ../Test/words.txt  
Groups should be in ../Test/tagGroups.txt  
Picture will be saved in ../Test/result.png
Words will be red in rectangles on white screen 

## Draw Settings: 
Only Words == 0  
Words In Rectangles == 1  
Only Rectangles == 2  
Rectangles With Numeration == 3  

## Tag groups:
Format: [Name] [minVal(double with dot)]-[maxVal(double with dot)] [font size];  
minVal-maxVal is segment in 0..1, which shows which words will be included.  
To decide that, programm will count for each word how many times it has come up in the text.  
Then it will take the maximum number(MaxFrequencyCount) and for each word determine a coefficient equal to wordFrequuencyCount /MaxFrequencyCount;  
So the most frequent word will have coefficient = 1.0;  
For example:  
Big 0.9-1.0 35;Others 0.0-0.9 25;  
In result will be 2 groups: Big and Others  
"Big" group will include words whose frequency coef >= MaxAppearanceCount * 0.9, so the most frequent word will be included in this group;  
"Others" group will include words whose frequency coef < MaxAppearanceCount * 0.9;  

# Result Examples:

###
Descending rectangles:
======
![Descending](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/descendingRectanglesTest.png?raw=true")

###
Random rectangles:
======
![Random](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/randomCloudTest.png?raw=true")

###
Similar rectangles:
======
![Similar](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/similarCloudTest.png?raw=true")

###
Kolobok test:
======
![Kolobok](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/result.png?raw=true")
