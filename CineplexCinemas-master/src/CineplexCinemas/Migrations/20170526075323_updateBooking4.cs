using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CineplexCinemas.Migrations
{
    public partial class updateBooking4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_CineplexMovie_SessionDetailsCineplexId_SessionDetailsMovieId_SessionDetailsSessionId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SessionDetailsCineplexId_SessionDetailsMovieId_SessionDetailsSessionId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SessionDetailsCineplexId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SessionDetailsMovieId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SessionDetailsSessionId",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "cineplxId",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "movieId",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sessionId",
                table: "Booking",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cineplxId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "movieId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "sessionId",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "SessionDetailsCineplexId",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionDetailsMovieId",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionDetailsSessionId",
                table: "Booking",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SessionDetailsCineplexId_SessionDetailsMovieId_SessionDetailsSessionId",
                table: "Booking",
                columns: new[] { "SessionDetailsCineplexId", "SessionDetailsMovieId", "SessionDetailsSessionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_CineplexMovie_SessionDetailsCineplexId_SessionDetailsMovieId_SessionDetailsSessionId",
                table: "Booking",
                columns: new[] { "SessionDetailsCineplexId", "SessionDetailsMovieId", "SessionDetailsSessionId" },
                principalTable: "CineplexMovie",
                principalColumns: new[] { "CineplexID", "MovieID", "SessionId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
