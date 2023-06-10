using Backups.Services;

namespace Backups.Algorithms;

public class SplitStorage : IAlgorithm
{
    public SplitStorage()
    {
    }

    public void CreateStorage(Backup backup, IRepository repository)
    {
        if (backup is null)
        {
            throw new ArgumentNullException(nameof(backup));
        }

        if (repository is null)
        {
            throw new ArgumentNullException(nameof(repository));
        }

        int counter = 0;
        foreach (IBackupObject backupObject in backup.RestorePoints.Last().BackupObjects)
        {
            counter++;
            repository.CreateZipArchive("Storage" + counter.ToString() + ".zip");
            repository.Storages.Add("Storage" + counter.ToString() + ".zip");
        }
    }
}
