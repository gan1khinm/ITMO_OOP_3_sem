using Backups.Services;

namespace Backups;

public class BackupDirectory : IBackupObject
{
    public BackupDirectory(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
    }

    public string Name { get; private set; }
}
