using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Controllers

{
    [ApiController]
    [Route("[controller]")]

    public class RezervacijaController : ControllerBase
    {
                public MuzickiFestivaliContext Context { get; set; }

        public RezervacijaController(MuzickiFestivaliContext context)
        {
              Context = context;
        }

        [Route("Pretraga/{festivalId}/{email}")]
        [HttpGet]

        public async Task<ActionResult> Pretraga(int festivalId,string email){

              if(string.IsNullOrEmpty(email))
            {
                return BadRequest("Nevalidan email");
            }
            
            try{
                var a=await Context.Rezervacije.Include(p=>p.Karte)
                .ThenInclude(p=>p.Dan)
                .ThenInclude(p=>p.Festival)
                .Where(p=>p.Email==email)
                .FirstOrDefaultAsync();
                if(a!=null)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest("Nije pronadjen email");
            }
        }
         catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("VratiSveMail/{festivalId}")]
        [HttpGet]

        public async Task<ActionResult> VratiSveMail(int festivalId){

                if(festivalId < 0){
                    return BadRequest("Pogresan ID");
                }
            try{
            
                var rez = await Context.Rezervacije.Where(p => p.Festival.ID == festivalId).ToListAsync();

                if(rez == null) {
                    return BadRequest("Nema rezervacija za festival");
                }

                return Ok(rez);
        }
        catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        
        
       /* [Route("Rezervisi/{Ime}/{Prezime}/{email}")]
        [HttpPost]
        public async Task<ActionResult> rezervisi(int kartaID,string Ime,string Prezime,string email){



            Rezervacija rez = new Rezervacija();
            rez.Ime = Ime;
            rez.Prezime = Prezime;
            rez.Email=email;
            //rez.Karte = Context.Karte.Find(kartaID);
    
            Context.Rezervacije.Add(rez);
            await Context.SaveChangesAsync();
            return Ok(rez);
        }
*/
      /*  [Route("Rezervisi/{Ime}/{Prezime}/{email}/{ukupnaCena}")]
        [HttpPost]

        public async Task<ActionResult> Rezervisi(int kartaID,string Ime,string Prezime,string email,int ukupnaCena){
            Rezervacija rez = new Rezervacija();
            rez.Ime = Ime;
            rez.Prezime = Prezime;
            rez.Email=email;
            rez.UkupnaCena=ukupnaCena;
            //rez.Karte = Context.Karte.Find(kartaID);
            Context.Rezervacije.Add(rez);
            await Context.SaveChangesAsync();
            return Ok("Uspesna rezervacija!");
        }*/
    


        [Route("NapraviRez/{Ime}/{Prezime}/{email}/{cena}/{festivalId}")]
        [HttpPost]
        public async Task<ActionResult> NapraviRez(string Ime,string Prezime,string email, int cena, int festivalId){


               if(cena<=0 || string.IsNullOrWhiteSpace(Ime) || Ime.Length>50 || string.IsNullOrWhiteSpace(Prezime) || Prezime.Length>50 || string.IsNullOrWhiteSpace(email) ||email.Length>80)
            {
                return BadRequest("Nevalidno") ;
            }
            try{
           
            var reze = Context.Rezervacije.Where(p => p.Email == email && p.Festival.ID == festivalId).FirstOrDefault();
            if(reze == null){
            var rez = new Rezervacija{
                Ime = Ime,
                Prezime = Prezime,
                Email=email,
                UkupnaCena = cena,
                Festival = Context.Festivali.Where(p=> p.ID == festivalId).FirstOrDefault()
            };
            //rez.Karte = Context.Karte.Find(kartaID);
    
            Context.Rezervacije.Add(rez);
            await Context.SaveChangesAsync();
            return Ok(rez.Festival);
            }
            else{
                return BadRequest("Vec postoji rezervacija");
            }
        }
         catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

            [Route("ukloniRezervaciju/{email}")]
            [HttpDelete]       
            public async Task<ActionResult> ukloniRezervaciju( string email){
             
              if(string.IsNullOrWhiteSpace(email) || email.Length>80)
                {
                     return BadRequest("Nevalidno") ;
                }
            try
            {
               
                var data = Context.Rezervacije.Where(p => p.Email == email);
                if(data.Any()){
                    Context.Rezervacije.Remove(data.First());
                    await Context.SaveChangesAsync();
                    return Ok("Rezervacija je obrisana !");
                }
                else
                {
                      return NotFound("Ne postoji rezervacija sa unetim podacima !");
                }
            }
        catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [Route("menjajRezervaciju/{cena}/{ime}/{prezime}/{email}")]
        [HttpPut]
        public async Task<ActionResult> menjaj(int cena,string ime,string prezime,string email){
              
              
              if(cena<=0 || string.IsNullOrWhiteSpace(ime) || ime.Length>50 || string.IsNullOrWhiteSpace(prezime) || prezime.Length>50  || string.IsNullOrWhiteSpace(email)||  prezime.Length>80 )
                {
                    return BadRequest("Nevalidno") ;
                }
            try{
          
            var data = Context.Rezervacije.Where(p => p.Email == email ).FirstOrDefault();
            if(data == null)
                return BadRequest("Ne postoji podatak");

            data.Ime=ime;
            data.Prezime=prezime;
            data.UkupnaCena = cena;

            await Context.SaveChangesAsync();
            return Ok("Uspesno azurirani podaci !" + data.UkupnaCena);
        }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ///////////
       /* [Route("menjajRezervaciju/{ID}/{ime}/{prezime}/{email}/{cena}")]
        [HttpPut]
        public async Task<ActionResult> menjaj2(int ID,string email,string ime,string prezime,float cena){
            if(ID < 0)
                return BadRequest("Nevalidan ID !");

            var data = Context.Rezervacije.Find(ID);
            if(data == null)
                return NotFound("Ne postoji podatak sa ovim ID-jem");

            data.Email = email;
            data.Ime=ime;
            data.Prezime=prezime;
            data.UkupnaCena=cena;

            await Context.SaveChangesAsync();
            return Ok("Uspesno azurirani podaci !");
        }*/
    }

    }
