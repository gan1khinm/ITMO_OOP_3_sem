using Backups.Exceptions;
using Backups.Services;

namespace Backups;

public class RestorePoint
{
    private List<IBackupObject> backupObjects;
    private DateTime _date;
    private string _path;
    private string _name;
    public RestorePoint(List<IBackupObject> objects, string path, string nameOfBackupTask, string nameOfRestorePoint, DateTime date)
    {
        if (objects is null)
        {
            throw new ArgumentNullException(nameof(objects));
        }

        if (System.IO.Path.Combine("Repository", nameOfBackupTask) != path)
        {
            throw new InvalidPathToDirectoryOrFile(path);
        }

        if (nameOfBackupTask is null)
        {
            throw new ArgumentNullException(nameof(nameOfBackupTask));
        }

        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        _name = nameOfBackupTask;
        _date = date;
        backupObjects = objects;
        _path = path;
    }

    public string NameOfRestorePoint => _name;
    public string Path => _path;
    public List<IBackupObject> BackupObjects => backupObjects;
    public DateTime Date => _date;
}
