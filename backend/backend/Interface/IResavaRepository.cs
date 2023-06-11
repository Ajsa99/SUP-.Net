using backend.Models;

namespace backend.Interface
{
    public interface IResavaRepository
    {
        Task<IEnumerable<Resava>> GetResavaAsync();
        Task<Resava> GetResavaAsync(int id);
        void AddResava(Resava dokument);
        void DeleteResava(int ResavaId);
        void UpdateResava(Resava resava);
        Task<Resava> FindResava(int id);
    }
}
