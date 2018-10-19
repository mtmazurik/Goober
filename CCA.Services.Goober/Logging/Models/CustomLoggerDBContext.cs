using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using CCA.Services.Goober.Models;

namespace CCA.Services.Goober.Logging.Models
{
    public class CustomLoggerDBContext : DbContext
    {
        public virtual DbSet<EventLog> EventLog { get; set; }
        public virtual DbSet<PeanutButter> Student { get; set; }
        public static string ConnectionString { get; set; }
        // public static int MessageMaxLength;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.LogLevel).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(4000);
            });
            modelBuilder.Entity<PeanutButter>(entity =>
            {
                entity.Property(e => e.PeanutButterId).HasColumnName("PeanutButterId");

                entity.Property(e => e.Brand).HasMaxLength(50);
                
            });
            // MessageMaxLength = 4000;
        }
    }
}
