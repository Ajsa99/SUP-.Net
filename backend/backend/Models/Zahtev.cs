using backend.Dtos;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Zahtev
    {
        public int Id { get; set; }
        public string Svrha { get; set; }
        public string Opis { get; set; }
        public string Status { get; set; }
        public string Hitno { get; set; }
        public string? Napomena { get; set; }
        public int idKorisnik { get; set; }

    }
}
