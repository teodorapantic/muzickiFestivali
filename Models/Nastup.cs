using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Nastup{

        [Key]
        public int ID { get; set; }
         [Required]
        public string ImeIzvodjaca { get; set; }

        [Required]
        public DateTime vreme { get; set; }

        [JsonIgnore]
        public  virtual DanFest Dan { get; set; }
        
    }
}