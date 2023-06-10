using Backups.Algorithms;
using Backups.Services;
using Xunit;

namespace Backups.Test;

public class BackupServiceTest
{
    [Fact]
    public void StartBackupTaskUsingSplitStorage_RestorePointsAndStoragesAreCreated()
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

        Assert.Equal(2, backup.RestorePoints.Count);

        Assert.Equal(3, repository.Storages.Count);
    }

    [Fact]
    public void StartBackupTaskUsingSingletStorage_RestorePointsAndStoragesAreCreated()
    {
        var backup = new Backup("Backup2");

        var repository = new InMemoryRepository("Repository", backup);

        var backupObject1 = new BackupFile("1.zip");

        var backupObject2 = new BackupFile("2.zip");

        var listOfObjects = new List<IBackupObject>();

        listOfObjects.Add(backupObject1);

        listOfObjects.Add(backupObject2);

        IAlgorithm algorithm = new SingleStorage();

        var task = new BackupTask("Backup2", repository, backup, algorithm);

        task.AddObjects(listOfObjects);

        task.Start();

        Assert.Equal("Storage.zip", repository.Storages.Last());
    }
}
