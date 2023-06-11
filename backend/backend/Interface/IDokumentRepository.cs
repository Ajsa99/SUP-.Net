using backend.Models;

namespace backend.Interface
{
    public interface IDokumentRepository
    {
        Task<IEnumerable<Dokument>> GetDokumentAsync();
        Task<Dokument> GetDokumentAsync(int id);
        void AddDokument(Dokument dokument);
        void DeleteDokument(int DokumentId);
        Task<Dokument> FindDokument(int id);

    }
}
