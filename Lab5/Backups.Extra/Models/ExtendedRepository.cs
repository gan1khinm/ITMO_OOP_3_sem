using Backups.Services;

namespace Backups.Extra.Models;

public static class ExtendedRepository
{
    public static void DeleteOldRestorePointsByAmount(this IRepository repository, List<RestorePoint> restorePoints, int limit)
    {
        if (restorePoints is null)
        {
            throw new ArgumentNullException(nameof(restorePoints));
        }

        if (restorePoints.Count > limit)
        {
            for (int i = 0; i < restorePoints.Count - limit; i++)
            {
                repository.DeleteRestorePoint(restorePoints[i].NameOfRestorePoint);
                restorePoints.Remove(restorePoints[i]);
            }
        }
    }

    public static void DeleteOldRestorePointsByDate(this IRepository repository, List<RestorePoint> restorePoints, DateTime dateTime)
    {
        foreach (RestorePoint restorePoint in restorePoints)
        {
            if (restorePoint.Date < dateTime)
            {
                repository.DeleteRestorePoint(restorePoint.NameOfRestorePoint);
                restorePoints.Remove(restorePoint);
            }
        }
    }

    public static void HybridDeleteRestorePoints(this IRepository repository, List<RestorePoint> restorePoints, int n, DateTime dateTime)
    {
        if (restorePoints is null)
        {
            throw new ArgumentNullException(nameof(restorePoints));
        }

        DeleteOldRestorePointsByAmount(repository, restorePoints, n);
        DeleteOldRestorePointsByDate(repository, restorePoints, dateTime);
    }

    public static void Merge(this IRepository repository, string nameOfRestorePoint, Backup backup)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        if (backup is null)
        {
            throw new ArgumentNullException(nameof(backup));
        }

        repository.CreateRestorePointFolder(nameOfRestorePoint);

        var backupObjects = new List<IBackupObject>();

        foreach (RestorePoint restoreP in backup.RestorePoints)
        {
            repository.CreateZipArchive(restoreP.NameOfRestorePoint);
            foreach (IBackupObject backupObject in restoreP.BackupObjects)
            {
                backupObjects.Add(backupObject);
            }
        }

        var restorePoint = new RestorePoint(backupObjects, Path.Combine(repository.PathToRepository, backup.NameOfBackupTask), backup.NameOfBackupTask, "MergedRestorePoint", DateTime.Now);

        backup.AddRestorPoint(restorePoint);
    }
}
