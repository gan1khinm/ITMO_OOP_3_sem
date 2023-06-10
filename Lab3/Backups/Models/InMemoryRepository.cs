using Backups.Services;

namespace Backups;

public class InMemoryRepository : IRepository
{
    private List<string> storages;
    private List<Composite> backupTasks;
    private Backup _backup;
    private List<IBackupObject> backupObjects;
    public InMemoryRepository(string path, Backup backup)
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        backupObjects = new List<IBackupObject>();
        storages = new List<string>();
        PathToRepository = path;
        backupTasks = new List<Composite>();
        _backup = backup;
    }

    public Backup Backup => _backup;
    public string PathToRepository { get; }
    public List<string> Storages => storages;

    public List<IBackupObject> BackupObjects => backupObjects;

    public void CreateBackupTaskFolder(string nameOfBackupTask)
    {
        if (nameOfBackupTask is null)
        {
            throw new ArgumentNullException(nameof(nameOfBackupTask));
        }

        var backupTask = new Composite(nameOfBackupTask, Path.Combine(PathToRepository, nameOfBackupTask));
        backupTasks.Add(backupTask);
    }

    public void CreateRestorePointFolder(string nameOfRestorePoint)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        var restorePoint = new Composite(nameOfRestorePoint, Path.Combine(backupTasks.Last().Path, nameOfRestorePoint));
        backupTasks.Last().Add(restorePoint);
    }

    public void CreateZipArchive(string nameOfZipArchive)
    {
        if (nameOfZipArchive is null)
        {
            throw new ArgumentNullException(nameof(nameOfZipArchive));
        }

        var zipArchive = new Leaf(nameOfZipArchive, Path.Combine(backupTasks.Last().Children.Last().Path, nameOfZipArchive));
        backupTasks.Last().Children.Add(zipArchive);
    }

    public void DeleteRestorePoint(string nameOfRestorePoint)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        foreach (Component component in backupTasks.Last().Children)
        {
            if (component.Name == nameOfRestorePoint)
            {
                backupTasks.Last().Remove(component);
            }
        }
    }
}
