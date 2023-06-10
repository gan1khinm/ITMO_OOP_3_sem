using Backups.Services;

namespace Backups;

public class BackupTask
{
    private Backup _backup;
    private IAlgorithm _algorithm;
    private IRepository _repository;

    private int counter;
    public BackupTask(string name, IRepository repository, Backup backup, IAlgorithm algorithm)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (repository is null)
        {
            throw new ArgumentNullException(nameof(repository));
        }

        if (backup is null)
        {
            throw new ArgumentNullException(nameof(backup));
        }

        if (algorithm is null)
        {
            throw new ArgumentNullException(nameof(algorithm));
        }

        _repository = repository;
        _backup = backup;

        _algorithm = algorithm;
        Name = name;
        _repository.CreateBackupTaskFolder(name);
        counter = 0;
    }

    public IRepository GetRepository => _repository;
    public string Name { get; set; }

    public Backup GetBackup => _backup;

    public void AddObjects(List<IBackupObject> objects)
    {
        if (objects is null)
        {
            throw new ArgumentNullException(nameof(objects));
        }

        counter++;
        _repository.CreateRestorePointFolder("restorePoint" + counter.ToString());
        var restorePoint = new RestorePoint(objects, Path.Combine(_repository.PathToRepository, Name), Name, "restorePoint" + counter.ToString(), DateTime.Now);
        _backup.AddRestorPoint(restorePoint);
    }

    public List<IBackupObject> DeleteObject(IBackupObject backoupObject)
    {
        if (backoupObject is null)
        {
            throw new ArgumentNullException(nameof(backoupObject));
        }

        var newBackupObjects = new List<IBackupObject>();

        foreach (IBackupObject object_ in _backup.RestorePoints.Last().BackupObjects)
        {
            if (object_ != backoupObject)
            {
                newBackupObjects.Add(object_);
            }
        }

        counter++;
        _repository.CreateRestorePointFolder("restorePoint" + counter.ToString());
        var restorePoint = new RestorePoint(newBackupObjects, Path.Combine(_repository.PathToRepository, Name), Name, "restorePoint" + counter.ToString(), DateTime.Now);
        _backup.AddRestorPoint(restorePoint);
        return newBackupObjects;
    }

    public void Start()
    {
        _algorithm.CreateStorage(_backup, _repository);
    }
}
