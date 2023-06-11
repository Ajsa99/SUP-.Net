using backend.Data;
using backend.Dtos;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentController : Controller
    {
        private readonly IUnitOfWork uow;

        private readonly DataContext dc;
        public DokumentController(IUnitOfWork uow, DataContext dc)
        {
            this.uow = uow;
            this.dc = dc;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDokument()
        {
            var dokument = await uow.DokumentRepository.GetDokumentAsync();

            var dokumentGetDto = from d in dokument
                                 select new DokumentGetDto()
                                 {
                                     idZahtev = d.idZahtev,
                                     Tip = d.Tip,
                                     Fajl = d.Fajl,
                                     Fajl2 = d.Fajl2,
                                     Fajl3 = d.Fajl3,
                                 };

            return Ok(dokumentGetDto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDokument(int id)
        {
            var dokument = await uow.DokumentRepository.FindDokument(id);

            if (dokument == null)
            {
                return NotFound();
            }

            var dokumentGetDto = new DokumentGetDto()
            {
                idZahtev = dokument.idZahtev,
                Tip = dokument.Tip,
                Fajl = dokument.Fajl,
                Fajl2 = dokument.Fajl2,
                Fajl3 = dokument.Fajl3,
                datum_dokumenta = dokument.datum_dokumenta
            };

            return Ok(dokumentGetDto);
        }


        [HttpPost("post")]
        public async Task<IActionResult> AddDokument(DokumentDto dokumentDto)
        {

            var dokument = new Dokument
            {
                Tip = dokumentDto.Tip,
                Fajl = dokumentDto.Fajl,
                Fajl2 = dokumentDto.Fajl2,
                Fajl3 = dokumentDto.Fajl3,
                datum_dokumenta = DateTime.Now
            };

            uow.DokumentRepository.AddDokument(dokument);
            await uow.SaveAsync();
            return StatusCode(201);
        }

    }




}
