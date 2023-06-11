using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repo
{
    public class ResavaRepository : IResavaRepository
    {
        private DataContext dc;

        public ResavaRepository(DataContext dc)
        {
            this.dc = dc;
        }


        public void AddResava(Resava resava)
        {
            if (dc.Resava is null)
            {
                throw new InvalidOperationException("The 'Resava' property is null.");
            }

            dc.Resava.AddAsync(resava);
        }
         public void DeleteResava(int ResavaId)
        {
            // var city = dc.Cities.Find(CityId);
            // dc.Cities.Remove(city);
            if (dc.Resava is null)
            {
                throw new InvalidOperationException("The 'Resava' property is null.");
            }

            var city = dc.Resava.Find(ResavaId);
            if (city is null)
            {
                throw new ArgumentException("The specified city does not exist.", nameof(ResavaId));
            }

            dc.Resava.Remove(city);
            dc.SaveChanges();
        }

        public void UpdateResava(Resava resava)
        {
            dc.Entry(resava).State = EntityState.Modified;
        }

        public async Task<Resava> FindResava(int id)
        {
            if (dc.Resava is null)
            {
                throw new InvalidOperationException("The 'Resava' property is null.");
            }
            var resava = await dc.Resava.FindAsync(id);
            if (resava == null)
            {
                throw new Exception("Resava not found"); // Možete prilagoditi izuzetak prema vašim potrebama
            }
            return resava;
        }

        public async Task<IEnumerable<Resava>> GetResavaAsync()
        {
            if (dc.Resava is null)
            {
                throw new InvalidOperationException("The 'Resava' property is null.");
            }

            return await dc.Resava.ToListAsync();
        }

        public async Task<Resava> GetResavaAsync(int id)
        {
            var resavaa = await GetResavaAsync();

            var resava = resavaa.FirstOrDefault(z => z.Id == id); 

            return resava;
        }
    }
}
