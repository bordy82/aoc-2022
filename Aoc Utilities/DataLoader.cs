namespace Utilities;

public class DataLoader
{
    public static int[] GetIntDataFromFile(bool isTest = false)
    {
        return GetStringDataFromFile(isTest).Select(line => Int32.Parse(line)).ToArray();
    }

    public static string[] GetStringDataFromFile(bool isTest = false)
    {
        var fileName = isTest ? "Test" : "Puzzle";
        return File.ReadAllLines($@"..\..\..\Data\{fileName}Input.txt");
    }
}
