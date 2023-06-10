using Backups.Extra.Abstractions;

namespace Backups.Extra.Models;

public class ConsoleLogger : ILogger
{
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
}
