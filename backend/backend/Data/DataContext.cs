using backend.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Zahtev>? Zahtev { get; set; }
        public DbSet<Dokument>? Dokument { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Resava>? Resava { get; set; }
        public DbSet<Termin>? Termin { get; set; }

    }
}
