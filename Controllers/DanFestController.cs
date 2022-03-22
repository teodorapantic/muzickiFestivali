
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Controllers

{
    [ApiController]
    [Route("[controller]")]

    public class DanFestController : ControllerBase
    {
                public MuzickiFestivaliContext Context { get; set; }

        public DanFestController(MuzickiFestivaliContext context)
        {
              Context = context;
        }


        [Route("PreuzmiLineUp/{naziv}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiLineUp(string naziv)
        { 
           if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
              return BadRequest("nije pravilno uneto");
          try
          {  
           
             var lineup = await Context.DaniFesta
             .Where(p=>p.Festival.Naziv==naziv)
             .Select( p=> new
             {
                 ID=p.ID,
                 Datum = p.datum,
                 Dan = p.Dan,
                 Cena = p.CenaZaDan,
                 Nastupi=p.Nastup
             }).ToListAsync();
            return Ok(lineup);
          }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
         
   
    }
}