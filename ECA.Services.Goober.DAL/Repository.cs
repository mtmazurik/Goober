using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ECA.Services.Goober.DAL.Models;

namespace ECA.Services.Goober.DAL
{
    public class Repository : IRepository
    {

        private string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private RepositoryContext InitializeContext()
        {
            var builder = new DbContextOptionsBuilder<RepositoryContext>();
            builder.UseSqlServer(_connectionString);
            return new RepositoryContext(builder.Options);
        }
        public PeanutButter ReadPeanutButter(int peanutButterId)
        {
            PeanutButter peanutButter;

            RepositoryContext context = InitializeContext();
            peanutButter = context.PeanutButters.Find(peanutButterId);
            context.Dispose();

            return peanutButter;
        }
        public List<Models.PeanutButter> ReadAllPeanutButters()
        {
            RepositoryContext context = InitializeContext();
            List<Models.PeanutButter> Goobers = new List<Models.PeanutButter>(context.PeanutButters.FromSql("Select * from PeanutButter"));
            context.Dispose();
            return Goobers;
        }
        public void CreatePeanutButter(Models.PeanutButter newPeanutButter)
        {
            RepositoryContext context = InitializeContext();            context.Add(newPeanutButter);
            context.SaveChanges();
            context.Dispose();
        }

        public void UpdatePeanutButter(PeanutButter peanutButter)
        {
            RepositoryContext context = InitializeContext();
            var result = context.PeanutButters.Find(peanutButter.PeanutButterId);
            if( result != null)
            {
                context.SaveChanges();
            }
            context.Dispose();
        }

    }
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions<RepositoryContext> options = null) : base(options)
        {
        }
        public virtual DbSet<Models.PeanutButter> PeanutButters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.PeanutButter>()
                .HasKey(p => p.PeanutButterId)
                .HasName("PeanutButter");
        }
    }

}
