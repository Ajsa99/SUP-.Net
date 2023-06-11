using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class LoginResDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}
