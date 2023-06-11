using backend.Data.Repo;
using backend.Interface;

namespace backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        public IUserRepository UserRepository =>
            new UserRepository(dc);

        public IZahtevRepository ZahtevRepository =>
            new ZahtevRepository(dc);

        public IDokumentRepository DokumentRepository => 
            new DokumentRepository(dc);

        public IResavaRepository ResavaRepository =>
            new ResavaRepository(dc);

        public ITerminRepository TerminRepository =>
            new TerminRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
