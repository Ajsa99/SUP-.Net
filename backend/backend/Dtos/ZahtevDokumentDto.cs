using backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Dtos
{
    public class ZahtevDokumentDto
    {
        public string Svrha { get; set; }
        public string Opis { get; set; }
        public string Status { get; set; }
        public string Hitno { get; set; }
        public string? Napomena { get; set; }
        public int idKorisnik { get; set; }
        public int idZahtev { get; set; }
        public string Tip { get; set; }
        [Required]
        public string Fajl { get; set; }
        [Required]
        public string Fajl2 { get; set; }

        public string Fajl3 { get; set; }

    }
}
