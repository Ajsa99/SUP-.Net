using AutoMapper;
using backend.Data;
using backend.Dtos;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZahtevController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        private readonly DataContext dc;

        public ZahtevController(IUnitOfWork uow, DataContext dc)
        {
            this.uow = uow;
            this.dc = dc;
        }

        //GET api/zahtev
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetZahtev()
        {
            var zahtev = await uow.ZahtevRepository.GetZahtevAsync();

            var zahtevDto = from z in zahtev
                            select new ZahtevDto()
                            {
                                Id = z.Id,
                                Svrha = z.Svrha,
                                Opis = z.Opis,
                                Status = z.Status,
                                Hitno = z.Hitno,
                                Napomena = z.Napomena,
                                idKorisnik = z.idKorisnik
                            };

            return Ok(zahtevDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetZahtev(int id)
        {
            var zahtev = await uow.ZahtevRepository.GetZahtevAsync(id);

            if (zahtev == null)
            {
                return NotFound(); // Ako zahtev sa datim ID-om ne postoji, vraćamo NotFound status
            }

            var zahtevGetDto = new ZahtevGetDto()
            {
                Id = zahtev.Id,
                Svrha = zahtev.Svrha,
                Opis = zahtev.Opis,
                Status = zahtev.Status,
                Hitno = zahtev.Hitno,
                Napomena = zahtev.Napomena,
                idKorisnik = zahtev.idKorisnik
            };

            return Ok(zahtevGetDto);
        }



        [HttpPost("post")]
        public async Task<IActionResult> AddZahtev(ZahtevDokumentDto zahtevDto)
        {
            var zahtev = new Zahtev
            {
                Svrha = zahtevDto.Svrha,
                Opis = zahtevDto.Opis,
                Status = zahtevDto.Status,
                Hitno = zahtevDto.Hitno,
                Napomena = zahtevDto.Napomena ?? string.Empty,
                idKorisnik = zahtevDto.idKorisnik
            };

            uow.ZahtevRepository.AddZahtev(zahtev);
            await uow.SaveAsync();

            var dokument = new Dokument
            {
                idZahtev = zahtevDto.idZahtev,
                Tip = zahtevDto.Tip,
                Fajl = zahtevDto.Fajl,
                Fajl2 = zahtevDto.Fajl2,
                Fajl3 = zahtevDto.Fajl3,
                datum_dokumenta = DateTime.Now
            };

            uow.DokumentRepository.AddDokument(dokument);
            await uow.SaveAsync();

            return StatusCode(201);
        }

        [HttpGet("korisnik/{iKorisnik}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Zahtev>>> GetZahtevByKorisnik(int iKorisnik)
        {
            var zahtevi = await dc.Zahtev.Where(zahtev => zahtev.idKorisnik == iKorisnik).ToListAsync();

            if (!zahtevi.Any())
            {
                return NotFound();
            }

            return zahtevi;
        }

        [HttpPut("statusUpdate/{id}")]
        public async Task<IActionResult> UpdateZahtevStatus(int id, ZahtevStatusDto statusDto)
        {
            var zahtev = await uow.ZahtevRepository.GetZahtevAsync(id);

            if (zahtev == null)
            {
                return NotFound();
            }

            zahtev.Status = statusDto.Status;

            uow.ZahtevRepository.UpdateZahtev(zahtev);
            await uow.SaveAsync();

            return NoContent();
        }


    }
}
