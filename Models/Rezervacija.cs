using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Rezervacija{
        
        [Key]
        public int ID { get; set; }

       
       /*[Required]
        [Range(1,300)]
        public int Ulaznica { get; set; }*/
 
        [Required]
        public float UkupnaCena { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(50)]    
        public string Prezime { get; set; }

        [MaxLength(80)]
        [EmailAddress]
        public string Email { get;set; }  
        
        public List<Karta> Karte { get; set; }
         
         [JsonIgnore]
         public  virtual Festival Festival { get; set; }
       

    }
}