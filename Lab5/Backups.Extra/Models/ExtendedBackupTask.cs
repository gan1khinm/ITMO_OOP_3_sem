using Backups.Services;

namespace Backups.Extra.Models;

public static class ExtendedBackupTask
{
    public static List<IBackupObject> RecoveringRPToOriginalLocation(this BackupTask backupTask, string nameOfRestorePoint)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        var backupObjects = new List<IBackupObject>();

        foreach (RestorePoint restorePoint in backupTask.GetRepository.Backup.RestorePoints)
        {
            if (restorePoint.NameOfRestorePoint == nameOfRestorePoint)
            {
                foreach (IBackupObject storage in restorePoint.BackupObjects)
                {
                    var backupObject = new BackupFile(storage.Name);
                    backupObjects.Add(backupObject);
                    backupTask.GetRepository.BackupObjects.Add(backupObject);
                }
            }
        }

        return backupObjects;
    }

    public static List<IBackupObject> RecoveringRPToDifferentLocation(this BackupTask backupTask, string nameOfRestorePoint, IRepository differentRepository)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        if (differentRepository is null)
        {
            throw new ArgumentNullException(nameof(differentRepository));
        }

        var backupObjects = new List<IBackupObject>();

        foreach (RestorePoint restorePoint in backupTask.GetRepository.Backup.RestorePoints)
        {
            if (restorePoint.NameOfRestorePoint == nameOfRestorePoint)
            {
                foreach (IBackupObject storage in restorePoint.BackupObjects)
                {
                    var backupObject = new BackupFile(storage.Name);
                    backupObjects.Add(backupObject);
                    differentRepository.BackupObjects.Add(backupObject);
                }
            }
        }

        return backupObjects;
    }
}
