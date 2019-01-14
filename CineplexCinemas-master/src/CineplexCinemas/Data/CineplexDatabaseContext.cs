using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CineplexCinemas.Models;

namespace CineplexCinemas.Models
{
    public partial class CineplexDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CineplexDatabase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cineplex>(entity =>
            {
                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();
            });

            modelBuilder.Entity<CineplexMovie>(entity =>
            {
                entity.HasKey(e => new { e.CineplexId, e.MovieId, e.SessionId })
                    .HasName("PK__Cineplex__CB419E6DA1BDF631");

                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Cineplex)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.CineplexId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Cinep__6FE99F9F");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Movie__70DDC3D8");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.SessionId);
            });

            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.Property(e => e.EnquiryId).HasColumnName("EnquiryID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Message).IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<MovieComingSoon>(entity =>
            {
                entity.Property(e => e.MovieComingSoonId).HasColumnName("MovieComingSoonID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasIndex(e => e.FilmMovieId)
                    .HasName("IX_Session_filmMovieId");

                entity.Property(e => e.FilmMovieId).HasColumnName("filmMovieId");

                entity.HasOne(d => d.FilmMovie)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.FilmMovieId);
            });
        }

        public virtual DbSet<Cineplex> Cineplex { get; set; }
        public virtual DbSet<CineplexMovie> CineplexMovie { get; set; }
        public virtual DbSet<Enquiry> Enquiry { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieComingSoon> MovieComingSoon { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public DbSet<Booking> Booking { get; set; }
    }
}