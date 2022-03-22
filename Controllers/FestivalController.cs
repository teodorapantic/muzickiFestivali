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
public class FestivalController : ControllerBase
{
    public MuzickiFestivaliContext Context { get; set; }
    public FestivalController(MuzickiFestivaliContext context)
    {
        Context = context;
    }



[Route("Festivali")]
[HttpGet]
public async Task<ActionResult> VratiFestivale()
{
   try 
    {
        var festivali = await Context.Festivali
		.Select(p => new {
			p.ID, 
			p.Naziv,
			p.Adresa,
			p.Grad,
			p.opisFestivala,
			datumPocetka = p.DatumPocetka.ToShortDateString(),
			datumDatumKraja = p.DatumKraja.ToShortDateString()

		}).ToListAsync();
		/*;
        var festival = await festivali.ToListAsync();*/
        return Ok(festivali);
    }   
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}
     


}
}