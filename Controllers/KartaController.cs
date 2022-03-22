using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace bioskop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KartaController : ControllerBase
    {
        public MuzickiFestivaliContext Context { get; set; }

        public KartaController(MuzickiFestivaliContext context)
        {
            Context = context;
        }

        [Route("PreuzmiKartu/{festivalid}")]
        [HttpGet]
        [EnableCors("CORS")]
        public async Task<ActionResult> PreuzmiKartu(int festivalid)
        {
             if(festivalid <= 0)
                return BadRequest("Nevalidan ID !");

            try{
            
             var karte =await Context.Karte
             .Include(p =>p. Dan)
             .ThenInclude(p=>p.Festival)
             .Where(p => p.Dan.Festival.ID==festivalid).ToListAsync();             
             
           
            return Ok(karte);
        }
         catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

////
    [Route("Karte")]
    [HttpGet]
    public async Task<ActionResult> VratiKarte()
    {
    try 
        {
            var karte = Context.Karte;
            var karta = await karte.ToListAsync();
            return Ok(karta);
        }   
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("BrisiKartu/{email}")]
    [HttpDelete]

    public async Task<ActionResult> BrisiKartu(string email){
         if(string.IsNullOrWhiteSpace(email))
               return BadRequest("Nevalidno");
        try{
           
        var rez = await Context.Rezervacije.Where(p => p.Email == email).FirstOrDefaultAsync();
        if(rez == null){
            return BadRequest("ne postoji rezervacija");
        }
        var kar = await Context.Karte.Where(p => p.Rezervacija == rez).FirstOrDefaultAsync();
                if(kar == null){
            return BadRequest("ne postoje karte");
        }
        while(kar != null){
            Context.Karte.Remove(kar);
            await Context.SaveChangesAsync();
            kar = await Context.Karte.Where(p => p.Rezervacija == rez).FirstOrDefaultAsync();
        }
        return Ok("obrisan");
    }
     catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }



    [Route("DodajKartu/{brojUlaznica}/{danNaziv}/{email}/{festivalid}")]
    [HttpPost]
    public async Task<ActionResult> DodajKartu(int brojUlaznica, string danNaziv, string email, int festivalid)

    {

         if(  brojUlaznica>5 || festivalid<=0 ||  string.IsNullOrWhiteSpace(email)||string.IsNullOrWhiteSpace(danNaziv))
               return BadRequest("Nevalidno");


            try{
                 
           
                var dan=await Context.DaniFesta.Where(p=>p.Dan==danNaziv).FirstOrDefaultAsync();
                if(dan==null)
                {
                    return BadRequest("Nema dana sa zadatim nazivom");
                }
                var rezervacija=await Context.Rezervacije.Where(p=>p.Email==email && p.Festival.ID == festivalid).FirstOrDefaultAsync();
                if(rezervacija==null)
                {
                    return BadRequest("Nema rezervacije sa zadatim id-em");

                }        
                var  k = new Karta(){
                    Ulaznica=brojUlaznica,
                    Dan=dan,
                    Rezervacija=rezervacija
                };
                Context.Karte.Add(k);
                await Context.SaveChangesAsync();
                 return Ok("Uspesno dodata karta!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);

            }

    }  

    }
}