using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandomContent.Entities;

namespace RandomContent.Database.Configuration
{
    /// <summary>
    /// Configuration fo User entities to store in database
    /// </summary>
    public class UserContextConfiguration : IEntityBuilder
    {
        /// <summary>
        /// Configure User entity
        /// </summary>
        /// <param name="entityBuilder"></param>
        private void UserConfiguration(EntityTypeBuilder<User> entityBuilder)
        {
            //set table
            entityBuilder.ToTable("users");

            //set entity
            entityBuilder.Property(x => x.Username).IsRequired().HasColumnName("username");
            entityBuilder.Property(x => x.Password).IsRequired().HasColumnName("password");
            entityBuilder.Property(x => x.Role).IsRequired().HasColumnName("access");

            //set primary key
            entityBuilder.HasKey(x => x.Id);
        }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserConfiguration);
        }
    }
}