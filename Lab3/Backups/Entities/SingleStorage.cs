using Backups.Services;

namespace Backups.Algorithms;

public class SingleStorage : IAlgorithm
{
    public SingleStorage()
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

        repository.CreateZipArchive("Storage.zip");
        repository.Storages.Add("Storage.zip");
    }
}
