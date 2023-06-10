using System.Text;
using System.Text.Json;

namespace Backups.Extra.Saving;

public static class BackupJobLoader
{
    public static void SaveBackupTask(BackupTask backupTask, string path)
    {
        if (backupTask == null)
        {
            throw new ArgumentNullException(nameof(backupTask));
        }

        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        string json = JsonSerializer.Serialize(backupTask, typeof(BackupTask));

        File.WriteAllText(path, json);
    }

    public static BackupTask? LoadBackupTask(string path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        string json = Encoding.Unicode.GetString(File.ReadAllBytes(path));

        BackupTask? backupTask = JsonSerializer.Deserialize<BackupTask>(json);

        return backupTask;
    }
}