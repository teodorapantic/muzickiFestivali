using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Festival
{   
    
    [Key]
    public int ID { get; set; }

    [MaxLength(80)]
    [Required]
    public string Adresa { get; set; }

    [MaxLength(50)]
    [Required]
    public string Grad { get; set; }

    [MaxLength(50)]
    [Required]
    public  string Naziv { get; set; }
    [Required]
    public string opisFestivala { get; set; }

    [Required]
    public DateTime DatumPocetka { get; set; }
    [Required]
    
     public DateTime DatumKraja { get; set; }
    [JsonIgnore]
    public List<DanFest> Dani { get; set; } 
    [JsonIgnore]
    public List<Rezervacija> Rezervacija { get; set; }


}
}