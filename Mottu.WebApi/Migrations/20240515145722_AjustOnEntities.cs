using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AjustOnEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressEntity");

            migrationBuilder.DropTable(
                name: "CityEntity");

            migrationBuilder.DropTable(
                name: "StateEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "MotorcycleEntityId",
                table: "ServiceEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MotorcycleEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Plate = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorcycleEntity_ClientEntity_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntity_MotorcycleEntityId",
                table: "ServiceEntity",
                column: "MotorcycleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorcycleEntity_ClientId",
                table: "MotorcycleEntity",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntity_MotorcycleEntity_MotorcycleEntityId",
                table: "ServiceEntity",
                column: "MotorcycleEntityId",
                principalTable: "MotorcycleEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntity_MotorcycleEntity_MotorcycleEntityId",
                table: "ServiceEntity");

            migrationBuilder.DropTable(
                name: "MotorcycleEntity");

            migrationBuilder.DropIndex(
                name: "IX_ServiceEntity_MotorcycleEntityId",
                table: "ServiceEntity");

            migrationBuilder.DropColumn(
                name: "MotorcycleEntityId",
                table: "ServiceEntity");

            migrationBuilder.CreateTable(
                name: "CityEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    StateEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    FederativeUnit = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    StateName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    StateEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdNumber = table.Column<string>(type: "text", nullable: true),
                    Complement = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Main = table.Column<bool>(type: "boolean", nullable: true),
                    NeighborhoodName = table.Column<string>(type: "text", nullable: true),
                    StreetName = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressEntity_CityEntity_CityEntityId",
                        column: x => x.CityEntityId,
                        principalTable: "CityEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AddressEntity_StateEntity_StateEntityId",
                        column: x => x.StateEntityId,
                        principalTable: "StateEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AddressEntity_UserEntity_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_CityEntityId",
                table: "AddressEntity",
                column: "CityEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_StateEntityId",
                table: "AddressEntity",
                column: "StateEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_UserEntityId",
                table: "AddressEntity",
                column: "UserEntityId");
        }
    }
}
