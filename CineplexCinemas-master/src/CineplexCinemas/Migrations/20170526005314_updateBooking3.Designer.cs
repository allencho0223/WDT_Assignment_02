using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CineplexCinemas.Models;

namespace CineplexCinemas.Migrations
{
    [DbContext(typeof(CineplexDatabaseContext))]
    [Migration("20170526005314_updateBooking3")]
    partial class updateBooking3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CineplexCinemas.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SessionDetailsCineplexId");

                    b.Property<int?>("SessionDetailsMovieId");

                    b.Property<int?>("SessionDetailsSessionId");

                    b.Property<string>("customerName");

                    b.Property<int>("numberOfAdults");

                    b.Property<int>("numberOfConc");

                    b.HasKey("BookingId");

                    b.HasIndex("SessionDetailsCineplexId", "SessionDetailsMovieId", "SessionDetailsSessionId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("CineplexCinemas.Models.Cineplex", b =>
                {
                    b.Property<int>("CineplexId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CineplexID");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("LongDescription")
                        .IsRequired();

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.HasKey("CineplexId");

                    b.ToTable("Cineplex");
                });

            modelBuilder.Entity("CineplexCinemas.Models.CineplexMovie", b =>
                {
                    b.Property<int>("CineplexId")
                        .HasColumnName("CineplexID");

                    b.Property<int>("MovieId")
                        .HasColumnName("MovieID");

                    b.Property<int>("SessionId");

                    b.HasKey("CineplexId", "MovieId", "SessionId")
                        .HasName("PK__Cineplex__CB419E6DA1BDF631");

                    b.HasIndex("CineplexId");

                    b.HasIndex("MovieId");

                    b.HasIndex("SessionId");

                    b.ToTable("CineplexMovie");
                });

            modelBuilder.Entity("CineplexCinemas.Models.Enquiry", b =>
                {
                    b.Property<int>("EnquiryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EnquiryID");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Message")
                        .IsRequired();

                    b.HasKey("EnquiryId");

                    b.ToTable("Enquiry");
                });

            modelBuilder.Entity("CineplexCinemas.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MovieID");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LongDescription")
                        .IsRequired();

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("MovieId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("CineplexCinemas.Models.MovieComingSoon", b =>
                {
                    b.Property<int>("MovieComingSoonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MovieComingSoonID");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LongDescription")
                        .IsRequired();

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("MovieComingSoonId");

                    b.ToTable("MovieComingSoon");
                });

            modelBuilder.Entity("CineplexCinemas.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FilmMovieId")
                        .HasColumnName("filmMovieId");

                    b.Property<int>("SeatsAvailable");

                    b.Property<int>("SeatsTotal");

                    b.Property<DateTime>("SessionDateTime");

                    b.HasKey("SessionId");

                    b.HasIndex("FilmMovieId")
                        .HasName("IX_Session_filmMovieId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("CineplexCinemas.Models.Booking", b =>
                {
                    b.HasOne("CineplexCinemas.Models.CineplexMovie", "SessionDetails")
                        .WithMany()
                        .HasForeignKey("SessionDetailsCineplexId", "SessionDetailsMovieId", "SessionDetailsSessionId");
                });

            modelBuilder.Entity("CineplexCinemas.Models.CineplexMovie", b =>
                {
                    b.HasOne("CineplexCinemas.Models.Cineplex", "Cineplex")
                        .WithMany("CineplexMovie")
                        .HasForeignKey("CineplexId")
                        .HasConstraintName("FK__CineplexM__Cinep__6FE99F9F");

                    b.HasOne("CineplexCinemas.Models.Movie", "Movie")
                        .WithMany("CineplexMovie")
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__CineplexM__Movie__70DDC3D8");

                    b.HasOne("CineplexCinemas.Models.Session", "Session")
                        .WithMany("CineplexMovie")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CineplexCinemas.Models.Session", b =>
                {
                    b.HasOne("CineplexCinemas.Models.Movie", "FilmMovie")
                        .WithMany("Session")
                        .HasForeignKey("FilmMovieId");
                });
        }
    }
}
