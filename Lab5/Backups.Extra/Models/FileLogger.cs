using Backups.Extra.Abstractions;

namespace Backups.Extra.Models;

public class FileLogger : ILogger
{
    public void PrintMessage(string message)
    {
        using var writer = new StreamWriter(@"./../../../Logging.txt", true);
        writer.WriteLine(message);
    }
}
