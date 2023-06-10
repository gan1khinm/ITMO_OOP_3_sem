namespace Backups.Exceptions;

public class InvalidPathToDirectoryOrFile : Exception
{
    public InvalidPathToDirectoryOrFile(string path)
    {
        Path = path;
        Console.WriteLine("File or directory doesn't exist");
    }

    public string Path { get; set; }
}
