
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infraestructure
{
    public class ApplicationContextTests : ApplicationContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("QualquercoisaAleatoria");
        }
    }
}