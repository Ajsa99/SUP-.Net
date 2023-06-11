using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Dokument
    {
        public int Id { get; set; }

        public int idZahtev { get; set; }
        public string Tip { get; set; }
        public string Fajl { get; set; }
        [Required]
        public string Fajl2 { get; set; }

        public string Fajl3 { get; set; }
 
        public DateTime datum_dokumenta { get; set; }
    }
}
