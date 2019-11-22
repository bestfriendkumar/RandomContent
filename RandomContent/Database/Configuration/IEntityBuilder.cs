using Microsoft.EntityFrameworkCore;

namespace RandomContent.Database.Configuration
{
    public interface IEntityBuilder
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }
}