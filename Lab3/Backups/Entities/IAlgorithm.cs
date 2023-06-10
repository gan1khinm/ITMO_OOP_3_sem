namespace Backups.Services;

public interface IAlgorithm
{
    public void CreateStorage(Backup backup, IRepository repository);
}
