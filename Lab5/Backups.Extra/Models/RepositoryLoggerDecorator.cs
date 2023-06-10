using Backups.Extra.Abstractions;
using Backups.Services;

namespace Backups.Extra.Models;
public class RepositoryLoggerDecorator : IRepository
{
    private readonly IRepository _inner;
    private readonly ILogger _logger;
    public RepositoryLoggerDecorator(IRepository inner, ILogger logger)
    {
        if (inner is null)
        {
            throw new ArgumentNullException(nameof(inner));
        }

        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        _inner = inner;
        _logger = logger;
    }

    public string PathToRepository => _inner.PathToRepository;

    public List<string> Storages => _inner.Storages;

    public Backup Backup => _inner.Backup;

    public List<IBackupObject> BackupObjects => _inner.BackupObjects;

    public void CreateBackupTaskFolder(string nameOfBackupTask)
    {
        if (nameOfBackupTask is null)
        {
            throw new ArgumentNullException(nameof(nameOfBackupTask));
        }

        _inner.CreateBackupTaskFolder(nameOfBackupTask);
        _logger.PrintMessage("Backup task folder " + nameOfBackupTask + "was created");
    }

    public void CreateRestorePointFolder(string nameOfRestorePoint)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        _inner.CreateRestorePointFolder(nameOfRestorePoint);
        _logger.PrintMessage("Restore point folder " + nameOfRestorePoint + "was created");
    }

    public void CreateZipArchive(string nameOfZipArchive)
    {
        if (nameOfZipArchive is null)
        {
            throw new ArgumentNullException(nameof(nameOfZipArchive));
        }

        _inner.CreateZipArchive(nameOfZipArchive);
        _logger.PrintMessage("Zip archive " + nameOfZipArchive + "was created");
    }

    public void DeleteRestorePoint(string nameOfRestorePoint)
    {
        if (nameOfRestorePoint is null)
        {
            throw new ArgumentNullException(nameof(nameOfRestorePoint));
        }

        _inner.DeleteRestorePoint(nameOfRestorePoint);
        _logger.PrintMessage("Restore point " + nameOfRestorePoint + "was deleted");
    }
}