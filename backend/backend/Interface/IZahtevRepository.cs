using backend.Models;

namespace backend.Interface
{
    public interface IZahtevRepository
    {
        Task<IEnumerable<Zahtev>> GetZahtevAsync();
        Task<Zahtev> GetZahtevAsync(int id);
        void AddZahtev(Zahtev zahtev);
        void DeleteZahtev(int ZahtevId);
        void UpdateZahtev(Zahtev zahtev);
        Task<Zahtev> FindZahtev(int id);
    }
}
