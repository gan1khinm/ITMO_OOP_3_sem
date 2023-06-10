namespace Backups;

public class Backup
{
    private List<RestorePoint> restorePoints;
    private string _nameOfBackupTask;
    public Backup(string nameOfBackupTask)
    {
        if (nameOfBackupTask is null)
        {
            throw new ArgumentNullException(nameof(nameOfBackupTask));
        }

        restorePoints = new List<RestorePoint>();
        _nameOfBackupTask = nameOfBackupTask;
    }

    public string NameOfBackupTask => _nameOfBackupTask;
    public IReadOnlyCollection<RestorePoint> RestorePoints { get { return restorePoints; } set { } }
    public void AddRestorPoint(RestorePoint restorePoint)
    {
        if (restorePoint is null)
        {
            throw new ArgumentNullException(nameof(restorePoint));
        }

        restorePoints.Add(restorePoint);
    }
}
