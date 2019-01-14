using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CineplexCinemas.Migrations
{
    public partial class updateBooking3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numberOfAdults",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numberOfConc",
                table: "Booking",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerName",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "numberOfAdults",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "numberOfConc",
                table: "Booking");
        }
    }
}
