using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repo
{
    public class TerminRepository : ITerminRepository
    {
        private DataContext dc;

        public TerminRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void AddTermin(Termin termin)
        {
            if (dc.Termin is null)
            {
                throw new InvalidOperationException("The 'Termin' property is null.");
            }

            dc.Termin.AddAsync(termin);
        }

        public async Task<Termin> FindTermin(int id)
        {
            if (dc.Termin is null)
            {
                throw new InvalidOperationException("The 'Termin' property is null.");
            }
            var termin = await dc.Termin.FindAsync(id);
            if (termin == null)
            {
                throw new Exception("Termin not found"); // Možete prilagoditi izuzetak prema vašim potrebama
            }
            return termin;
        }


        public async Task<IEnumerable<Termin>> GetTerminAsync()
        {
            if (dc.Termin is null)
            {
                throw new InvalidOperationException("The 'Termin' property is null.");
            }

            return await dc.Termin.ToListAsync();
        }

        public async Task<Termin> GetTerminAsync(int id)
        {
            var termini = await GetTerminAsync();

            var termin = termini.FirstOrDefault(z => z.Id == id);

            return termin;
        }
    }
}
