using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{
    public class Karta

    {
        [Key]
        public int ID { get; set;}

        
        [Required]
        [Range(1,5)]
        public int Ulaznica { get; set; }

        public virtual DanFest Dan { get; set; }
        [JsonIgnore]

        public virtual Rezervacija Rezervacija { get; set; }
       

    }
}