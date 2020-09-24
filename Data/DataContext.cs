using Microsoft.EntityFrameworkCore;
using APISendgrid.Models;

namespace APISendgrid.Data
{
  public class DataContext : DbContext
  {
      public DataContext(DbContextOptions<DataContext> options)
        : base(options)
      {}

      public DbSet<Email> Email {get; set;}
      public DbSet<ExampleTemplateData> TemplateData {get; set;}
  }
}