using System.Threading.Tasks;

namespace UniSyncClient.Model.Dependency
{
    public interface IFilesManagement
    {
        string StartFolder { get; }

        Task<string> GetFolderPath();
    }
}