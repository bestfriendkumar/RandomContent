using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RandomContent.Database.Configuration;
using RandomContent.Entities;

namespace RandomContent.Database.Context
{
    public sealed class AppDbContext : DbContext
    {
        /// <summary>
        /// Gets the database location
        /// </summary>
        public string DbLocation { get; private set; }

        public AppDbContext()
        {
            try
            {
                //location is the present directory
                var loc = ".";
                DbLocation = loc;
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set MMS database location.", ex);
            }
        }

        #region Entities

        public DbSet<User> Users { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //This will get all configuration assemblies
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(
                    t =>
                        t.FullName != null && (t.FullName.StartsWith("MMSDataProvider.Configuration") &&
                                               typeof(IEntityBuilder).IsAssignableFrom(t) && t.IsClass))
                .ToList();

            //Loops through each configuration and calls OnModelCreating for each configuration
            //See IEntityBuilder implementations for setting table configurations
            foreach (var type in types)
            {
                var instance = (IEntityBuilder)Activator.CreateInstance(type);
                instance.OnModelCreating(modelBuilder);
            }
        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string path;
            if (DbLocation != null)
            {
                Directory.CreateDirectory(DbLocation);
                path = Path.Combine(DbLocation, "app.db");
                DbLocation = path;
            }
            else
            {
                path = @".\app.db";
                DbLocation = path;
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = path
            };

            if (optionsBuilder == null || optionsBuilder.IsConfigured) return;

            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }

        #endregion
    }
}