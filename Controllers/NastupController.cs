using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NastupController : ControllerBase
    {
        public MuzickiFestivaliContext Context { get; set; }

        public NastupController(MuzickiFestivaliContext context)
        {
            Context = context;
        }


        [HttpGet("PreuzmiNastup/{id}")]
        public async Task<ActionResult<Nastup>> GetNastup(int id)
        {
            var nastup = await Context.Nastupi.FindAsync(id);
            if (nastup == null)
            {
                return NotFound();
            }
            return nastup;
        }
   


        private bool NastupExists(int id)
        {
            return Context.Nastupi.Any(e => e.ID == id);
        }


        [HttpPut("Izmeni/{id}")]
        public async Task<IActionResult> PutNastup(int id, Nastup nastup)
        {
            if (id != nastup.ID)
            {
                return BadRequest();
            }
            Context.Entry(nastup).State = EntityState.Modified;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NastupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }


        [HttpDelete("ObrisiNastup/{id}")]
        public async Task<IActionResult> DeleteNastup(int id)
        {
            var film = await Context.Nastupi.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            Context.Nastupi.Remove(film);
            await Context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost]
        [EnableCors("CORS")]
        [Route("DodajNastup/{ImeIzvodjaca}/{Vreme}/{Dan}/")]
        public async Task<ActionResult> DodajNastup(string ImeIzvodjaca, DateTime Vreme,string Dan/*string Imefestivala*/)
        {

             if(string.IsNullOrWhiteSpace(ImeIzvodjaca) || ImeIzvodjaca.Length>50)
            {
                return BadRequest("Lose unet naziv bolnice!");
            }

            try
            {


                var dan = await Context.DaniFesta
                    .Where(d => d.Dan == Dan)
                    .FirstOrDefaultAsync();
                /*var festival = await Context.Festivali
                    .Where(s => s.Naziv == Imefestivala)
                    .FirstOrDefaultAsync();*/
                if (/*festival == null*/ dan == null)
                {
                    return BadRequest();
                }
                var n = new Nastup
                {
                    ImeIzvodjaca= ImeIzvodjaca,
                    vreme = Vreme,
                   // vreme.ToShortTimeString(),
                    Dan=dan,
                   // Imefestivala=festival
                };
                Context.Nastupi.Add(n);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodat nastup {ImeIzvodjaca}");
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }




    }

}
        