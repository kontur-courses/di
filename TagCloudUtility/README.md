## Input:
Format: [pathToWords] [pathToPicture] [pathToGroups] [drawSettings = WordsInRectangles]  
(Paths starting from exe directory)

## Example of using:
Exe in C:.../Test/TagCloud.Utility.exe  
Input: words.txt result tagGroups.txt  
In result  
Words should be in ../Test/words.txt  
Groups should be in ../Test/tagGroups.txt  
Picture will be saved in ../Test/result.png  
(Picture will contain words in recntagles)  

## Draw Settings:
To change draw setting write in input number of choosed option  
Options:  
Only Words == 0  
Words In Rectangles == 1  
Only Rectangles == 2  
Rectangles With Numeration == 3  

## Tag groups:
Format: [Name] [minVal]-[maxVal] [width per letter]x[height];  
minVal-maxVal is segment in 0..1, which shows which words will be included.  
To decide that, programm will count for each word how many times it has come up in the text.  
Then it will take the maximum number(MaxFrequencyCount) and for each word determine a coefficient equal to wordFrequuencyCount /MaxFrequencyCount;  
So the most frequent word will have coefficient = 1;  
For example:  
Big 0.9-1 80x150;Others 0-0.9 50x100;  
In result will be 2 groups: Big and Others  
"Big" group will include words whose frequency coef >= MaxAppearanceCount * 0.9,so the most frequent word will be included in this group;  
"Others" group will include words whose frequency coef < MaxAppearanceCount * 0.9;  

# Result Examples:

###
Descending rectangles:
======
![Descending](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/descendingRectanglesTest.png?raw=true")

###
Random rectangles:
======
![Descending](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/randomCloudTest.png?raw=true")

###
Similar rectangles:
======
![Descending](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/similarCloudTest.png?raw=true")

###
Kolobok test:
======
![Descending](https://github.com/Rozentor/tdd/blob/master/cs/TagCloudUtility/result.png?raw=true")
