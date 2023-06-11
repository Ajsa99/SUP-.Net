using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repo
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly DataContext dc;

        public DokumentRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void AddDokument(Dokument dokument)
        {
            if (dc.Dokument is null)
            {
                throw new InvalidOperationException("The 'Dokument' property is null.");
            }

            dc.Dokument.AddAsync(dokument);
        }

        public void DeleteDokument(int DokumentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Dokument> FindDokument(int id)
        {
            if (dc.Dokument is null)
            {
                throw new InvalidOperationException("The 'Dokument' property is null.");
            }
            var dokument = await dc.Dokument.FindAsync(id);
            if (dokument == null)
            {
                throw new Exception("Dokument not found"); // Možete prilagoditi izuzetak prema vašim potrebama
            }
            return dokument;
        }


        public async Task<IEnumerable<Dokument>> GetDokumentAsync()
        {
            if (dc.Dokument is null)
            {
                throw new InvalidOperationException("The 'Dokument' property is null.");
            }

            return await dc.Dokument.ToListAsync();
        }

 
        public async Task<Dokument> GetDokumentAsync(int id)
        {
            var dokumenti = await GetDokumentAsync(); // Pozivamo postojeću metodu za dobijanje svih zahteva

            var dokument = dokumenti.FirstOrDefault(z => z.idZahtev== id); // Filtriramo zahteve po ID-u

            return dokument;
        }
    }
}
