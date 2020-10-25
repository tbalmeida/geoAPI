using geoAPI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace geoAPI.App
{
  public class ApplicationDbContext: IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
      {

      }

      public DbSet<Company> Companies { get; set; }
  }
}
