using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class LoginReq1Dto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
