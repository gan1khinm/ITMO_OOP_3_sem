using Backups.Services;

namespace Backups;

public class BackupFile : IBackupObject
{
    public BackupFile(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
    }

    public string Name { get; private set; }
}
