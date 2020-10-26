using Microsoft.EntityFrameworkCore;
using FilmeMvc.Models;

namespace FilmeMvc 
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> options)
            : base(options)
        {
        }
        public DbSet<Filme> Filmes { get; set; }
    }
}