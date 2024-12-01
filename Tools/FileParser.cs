public class FileParser
{
    public static string[] ParseFile(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException($"The file '{filename}' does not exist.");
        }

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filename);

        return lines;
    }
}