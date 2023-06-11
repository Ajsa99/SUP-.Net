namespace backend.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IZahtevRepository ZahtevRepository { get; }
        IDokumentRepository DokumentRepository { get; }
        IResavaRepository ResavaRepository { get; }
        ITerminRepository TerminRepository { get; }
        Task<bool> SaveAsync();
    }
}
