using backend.Models;

namespace backend.Interface
{
    public interface ITerminRepository
    {
        Task<IEnumerable<Termin>> GetTerminAsync();
        Task<Termin> GetTerminAsync(int id);
        void AddTermin(Termin termin);
        Task<Termin> FindTermin(int id);
    }
}
