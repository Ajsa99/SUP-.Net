using AutoMapper;
using backend.Data;
using backend.Dtos;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;

        public UserController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.uow = uow;
            this.configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var user = await uow.UserRepository.GetUserAsync();

            var userGetDto = from u in user
                             select new UserGetDto()
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 Lastname = u.Lastname,
                                 Email = u.Email,
                                 UserName = u.UserName,
                                 Mobile = u.Mobile,
                                 DateBirthday = u.DateBirthday,
                                 Gender = u.Gender,
                                 MaritalStatus = u.MaritalStatus,
                                 City = u.City,
                                 Address = u.Address,
                                 Type = u.Type
                             };

            return Ok(userGetDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await uow.UserRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound(); // Ako zahtev sa datim ID-om ne postoji, vraćamo NotFound status
            }

            var userGetDto = new UserGetDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Email = user.Email,
                UserName = user.UserName,
                Mobile = user.Mobile,
                DateBirthday = user.DateBirthday,
                Gender = user.Gender,
                MaritalStatus = user.MaritalStatus,
                City = user.City,
                Address = user.Address,
                Type = user.Type
            };

            return Ok(userGetDto);
        }

        // ...............

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginReq1Dto loginReq)
        {

            Console.WriteLine(loginReq.UserName);

            var user = await uow.UserRepository.Authenticate(loginReq.UserName!, loginReq.Password!);

            if (user == null)
            {
                return Unauthorized();
            }

            var login = new LoginResDto();
            login.UserName = user.UserName;
            login.Token = CreateJWT(user);
            return Ok(login);
        }

        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                     .GetBytes(secretKey!));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Type", user.Type),
            };

            var signingCredentials = new SigningCredentials(
               key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost("/register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            
            if (await uow.UserRepository.UserAlreadyExists(loginReq.UserName!))
            {
                return BadRequest("Korisnik sa " + loginReq.UserName + " korisničkim imenom već postoji.");
            }
            else if (await uow.UserRepository.UserAlreadyExists(loginReq.Email!))
            {
                return BadRequest("Korisnik sa emailom " + loginReq.Email + " već postoji.");
            }


            uow.UserRepository.Register(loginReq.FirstName!, loginReq.Lastname!, loginReq.Email!, loginReq.UserName!, (int)loginReq.Mobile, (DateTime)loginReq.DateBirthday, loginReq.Gender!,
                loginReq.MaritalStatus!, loginReq.City!, loginReq.Address!, loginReq.Password!, loginReq.Type!);
            await uow.SaveAsync();
            return Ok(loginReq);
        }
    }
}
