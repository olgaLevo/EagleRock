using EagleRock.Api.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace EagleRock.Api.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options) { }

        public DbSet<EagleBotDto> EagleBootRecords { get; set; }
    }
}
