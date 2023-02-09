using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Createhostusername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHost",
                table: "RoomAttendees");

            migrationBuilder.AddColumn<string>(
                name: "HostUsername",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostUsername",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsHost",
                table: "RoomAttendees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
