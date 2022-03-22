using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Models{

    public class MuzickiFestivaliContext : DbContext{
        public DbSet<Nastup> Nastupi {get;set;}
        public DbSet<Festival> Festivali { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Karta> Karte { get; set; }
        public DbSet<DanFest> DaniFesta { get; set; }
      
        public MuzickiFestivaliContext(DbContextOptions option) : base(option){

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }

}