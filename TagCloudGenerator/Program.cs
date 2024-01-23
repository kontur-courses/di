using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;

public class Program
{
    public static void Main(string[] args)
    {
        var filePath = @"C:\Users\lholy\Documents\GitHub\di\TagCloudGeneratorTest\TestsData\test1.txt";

        var text = File.ReadAllLines(filePath);
    }
}