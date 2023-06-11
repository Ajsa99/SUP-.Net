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
    public class TerminController : Controller
    {
        private readonly IUnitOfWork uow;

        private readonly DataContext dc;
        public TerminController(IUnitOfWork uow, DataContext dc)
        {
            this.uow = uow;
            this.dc = dc;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTermin()
        {
            var termin = await uow.TerminRepository.GetTerminAsync();

            var terminGetDto = from t in termin
                               select new TerminDto()
                               {
                                   IdResava = t.IdResava,
                                   datum_termina = t.datum_termina,
                                   vreme_termina = t.vreme_termina,
                               };

            return Ok(terminGetDto);
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddTermin(TerminDto terminDto)
        {

            var termin = new Termin
            {
                IdResava = terminDto.IdResava,
                datum_termina = terminDto.datum_termina,
                vreme_termina = terminDto.vreme_termina,

            };

            uow.TerminRepository.AddTermin(termin);
            await uow.SaveAsync();
            return StatusCode(201);
        }
    }
}
