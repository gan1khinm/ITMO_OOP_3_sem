namespace Backups.Services;

public interface IRepository
{
    string PathToRepository { get; }
    List<string> Storages { get; }
    Backup Backup { get; }
    List<IBackupObject> BackupObjects { get; }
    void CreateBackupTaskFolder(string nameOfBackupTask);
    void CreateRestorePointFolder(string nameOfRestorePoint);
    void CreateZipArchive(string nameOfZipArchive);
    void DeleteRestorePoint(string nameOfRestorePoint);
}
