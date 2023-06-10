using Backups.Algorithms;
using Backups.Extra.Models;
using Backups.Services;
using Xunit;

namespace Backups.Test;

public class BackupServiceTest
{
    [Fact]
    public void ClearRestorePointsByAmount_RestorePointsWereCleared()
    {
        var backup = new Backup("Backup1");

        var repository = new InMemoryRepository(Path.Combine("Repository"), backup);

        var backupObject1 = new BackupFile("1.zip");

        var backupObject2 = new BackupFile("2.zip");

        var listOfObjects = new List<IBackupObject>();

        listOfObjects.Add(backupObject1);

        listOfObjects.Add(backupObject2);

        IAlgorithm algorithm = new SplitStorage();

        var task = new BackupTask("Backup1", repository, backup, algorithm);

        task.AddObjects(listOfObjects);

        task.Start();

        task.DeleteObject(backupObject2);

        task.Start();

        repository.DeleteOldRestorePointsByAmount((List<RestorePoint>)repository.Backup.RestorePoints, 1);

        Assert.Equal(1, repository.Backup.RestorePoints.Count);
    }

    [Fact]
    public void MergeRestorePoints_RestorePointsWereMerged()
    {
        var backup = new Backup("Backup2");

        var repository = new InMemoryRepository(Path.Combine("Repository"), backup);

        var backupObject1 = new BackupFile("1.zip");

        var backupObject2 = new BackupFile("2.zip");

        var listOfObjects = new List<IBackupObject>();

        listOfObjects.Add(backupObject1);

        listOfObjects.Add(backupObject2);

        IAlgorithm algorithm = new SplitStorage();

        var task = new BackupTask("Backup2", repository, backup, algorithm);

        task.AddObjects(listOfObjects);

        task.Start();

        task.DeleteObject(backupObject2);

        task.Start();

        repository.Merge("NewResorePoint", backup);

        Assert.Equal(3, backup.RestorePoints.Count);
    }
}
