using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQEntityFrameworkCore.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
