using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace backend.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;

        }
        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await dc.Users?.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }

        public void Register(string firstName, string lastName, string email, string userName,int mobile, DateTime dateBirthday, string gender,
                string maritalStatus, string city, string address, string password, string type)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                User user = new User();
                user.FirstName = firstName;
                user.Lastname = lastName;
                user.Email = email;
                user.UserName = userName;
                user.Mobile = mobile;
                user.DateBirthday = dateBirthday;
                user.Gender = gender;
                user.MaritalStatus = maritalStatus;
                user.City = city;
                user.Address = address;
                user.Password = passwordHash;
                user.PasswordKey = passwordKey;
                user.Type = type;

                dc.Users.Add(user);
            }
        }

        //public void Register(string userName, string password)
        //{
        //    byte[] passwordHash, passwordKey;

        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordKey = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        //        User user = new User();
        //        user.UserName = userName;
        //        user.Password = passwordHash;
        //        user.PasswordKey = passwordKey;

        //        dc.Users.Add(user);
        //    }
        //}

        public async Task<bool> UserAlreadyExists(string firstName)
        {
            return await dc.Users.AnyAsync(x => x.FirstName == firstName);

        }

        // .............
        public async Task<User> FindUser(int id)
        {
            if (dc.Users is null)
            {
                throw new InvalidOperationException("The 'User' property is null.");
            }
            var user = await dc.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found"); // Možete prilagoditi izuzetak prema vašim potrebama
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            // return await dc.Cities.ToListAsync();
            if (dc.Users is null)
            {
                throw new InvalidOperationException("The 'Users' property is null.");
            }

            return await dc.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            var users = await GetUserAsync(); // Pozivamo postojeću metodu za dobijanje svih zahteva

            var user = users.FirstOrDefault(u => u.Id == id); // Filtriramo zahteve po ID-u

            return user;
        }
    }
}
