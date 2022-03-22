using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class DanFest
    {
        [Key]
        public int ID { get; set;}

        [Required]
        public string Dan { get; set; }
        public  virtual List<Nastup> Nastup{ get; set; }

        [Required]
        public float CenaZaDan { get; set; }

        public DateTime datum { get; set; }

        public virtual Festival Festival { get; set; }
        
        [JsonIgnore]
         public List<Karta> Karte { get; set; }
    }   
}