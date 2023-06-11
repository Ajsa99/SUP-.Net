using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Data.Repo
{
    public class ZahtevRepository : IZahtevRepository
    {
        private readonly DataContext dc;

        public ZahtevRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddZahtev(Zahtev zahtev)
        {
            // dc.Zahtev.AddAsync(zahtev);
            if (dc.Zahtev is null)
            {
                throw new InvalidOperationException("The 'Zahtev' property is null.");
            }

            dc.Zahtev.AddAsync(zahtev);
        }

        public void DeleteZahtev(int ZahtevId)
        {
            throw new NotImplementedException();
        }

        public async Task<Zahtev> FindZahtev(int id)
        {
            if (dc.Zahtev is null)
            {
                throw new InvalidOperationException("The 'Zahtev' property is null.");
            }
            var zahtev = await dc.Zahtev.FindAsync(id);
            if (zahtev == null)
            {
                throw new Exception("Zahtev not found"); // Možete prilagoditi izuzetak prema vašim potrebama
            }
            return zahtev;
        }



        public async Task<IEnumerable<Zahtev>> GetZahtevAsync()
        {
            // return await dc.Cities.ToListAsync();
            if (dc.Zahtev is null)
            {
                throw new InvalidOperationException("The 'Zahtev' property is null.");
            }

            return await dc.Zahtev.ToListAsync();
        }

        public async Task<Zahtev> GetZahtevAsync(int id)
        {
            var zahtevi = await GetZahtevAsync(); // Pozivamo postojeću metodu za dobijanje svih zahteva

            var zahtev = zahtevi.FirstOrDefault(z => z.Id == id); // Filtriramo zahteve po ID-u

            return zahtev;
        }

        public void UpdateZahtev(Zahtev zahtev)
        {
            dc.Entry(zahtev).State = EntityState.Modified;
        }



    }
}
