﻿namespace backend.Dtos
{
    public class LoginReqDto
    {
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public int? Mobile { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? Type { get; set; }

    }
}
