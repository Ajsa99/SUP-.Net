using backend.Data;
using backend.Dtos;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResavaController : Controller
    {
        private readonly IUnitOfWork uow;

        private readonly DataContext dc;
        public ResavaController(IUnitOfWork uow, DataContext dc)
        {
            this.uow = uow;
            this.dc = dc;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetResava()
        {
            var resava = await uow.ResavaRepository.GetResavaAsync();

            var resavaGetDto = from d in resava
                                 select new ResavaDto()
                                 {
                                     IdZahtev = d.IdZahtev,
                                     IdSluzbenik = d.IdSluzbenik,
                                     Status = d.Status
                                 };

            return Ok(resavaGetDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetResava(int id)
        {
            var resava = await uow.ResavaRepository.FindResava(id);

            if (resava == null)
            {
                return NotFound();
            }

            var resavaGetDto = new ResavaGetDto()
            {
                Id = resava.Id,
                IdZahtev = resava.IdZahtev,
                IdSluzbenik = resava.IdSluzbenik,
                Status = resava.Status,
            };

            return Ok(resavaGetDto);
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddResava(ResavaDto resavaDto)
        {

            var resava = new Resava
            {
                IdZahtev = resavaDto.IdZahtev,
                IdSluzbenik = resavaDto.IdSluzbenik,
                Status = resavaDto.Status

            };

            uow.ResavaRepository.AddResava(resava);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteResava(int id)
        {
            uow.ResavaRepository.DeleteResava(id);
            await uow.SaveAsync();
            return Ok(id);
        }

        [HttpPut("statusResava/{id}")]
        public async Task<IActionResult> UpdateZahtevStatus(int id, ResavaStatus statusDto)
        {
            var resava = await uow.ResavaRepository.GetResavaAsync(id);

            if (resava == null)
            {
                return NotFound();
            }

            resava.Status = statusDto.Status;

            uow.ResavaRepository.UpdateResava(resava);
            await uow.SaveAsync();

            return NoContent();
        }

    }
}
