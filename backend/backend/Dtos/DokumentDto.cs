using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Dtos
{
    public class DokumentDto
    {
        public string Tip { get; set; }
        [Required]
        public string Fajl { get; set; }
        [Required]
        public string Fajl2 { get; set; }
        public string Fajl3 { get; set; }
        public DateTime datum_dokumenta { get; set; }

    }
}