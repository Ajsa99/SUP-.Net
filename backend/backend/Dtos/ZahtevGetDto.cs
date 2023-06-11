namespace backend.Dtos
{
    public class ZahtevGetDto
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
