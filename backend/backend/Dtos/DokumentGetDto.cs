using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class DokumentGetDto    {
        public int idZahtev { get; set; }
        public string Tip { get; set; }
        [Required]
        public string Fajl { get; set; }
        [Required]
        public string Fajl2 { get; set; }
        public string Fajl3 { get; set; }
        public DateTime datum_dokumenta { get; set; }
    }
}
