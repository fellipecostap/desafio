using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    StateEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreRegistrationEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    TokenValidation = table.Column<string>(type: "text", nullable: true),
                    NickName = table.Column<string>(type: "text", nullable: true),
                    TagValidation = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreRegistrationEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateName = table.Column<string>(type: "text", nullable: true),
                    FederativeUnit = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserTypeName = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    GenreType = table.Column<int>(type: "integer", nullable: false),
                    CNHType = table.Column<int>(type: "integer", nullable: false),
                    UserCNH = table.Column<string>(type: "text", nullable: true),
                    UserCnpj = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserMail = table.Column<string>(type: "text", nullable: true),
                    UserPhone = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserNickName = table.Column<string>(type: "text", nullable: true),
                    UserExcluded = table.Column<bool>(type: "boolean", nullable: true),
                    UserTypeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    AcceptTermsDate = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntity_UserTypeEntity_UserTypeEntityId",
                        column: x => x.UserTypeEntityId,
                        principalTable: "UserTypeEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StateEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    NeighborhoodName = table.Column<string>(type: "text", nullable: true),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    StreetName = table.Column<string>(type: "text", nullable: true),
                    AdNumber = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    Complement = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Main = table.Column<bool>(type: "boolean", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ClientEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientUserEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfilePhoto = table.Column<string>(type: "text", nullable: true),
                    ClientExcluded = table.Column<bool>(type: "boolean", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEntity_UserEntity_ClientUserEntityId",
                        column: x => x.ClientUserEntityId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreUpdateEmailEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OldEmail = table.Column<string>(type: "text", nullable: false),
                    NewEmail = table.Column<string>(type: "text", nullable: false),
                    TokenValidation = table.Column<string>(type: "text", nullable: false),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreUpdateEmailEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreUpdateEmailEntity_UserEntity_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokenEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokenEntity_UserEntity_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Plan = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    Fine = table.Column<double>(type: "double precision", nullable: true),
                    InitialDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FinalDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PrevisionFinalDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceEntity_ClientEntity_ClientEntityId",
                        column: x => x.ClientEntityId,
                        principalTable: "ClientEntity",
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

            migrationBuilder.CreateIndex(
                name: "IX_ClientEntity_ClientUserEntityId",
                table: "ClientEntity",
                column: "ClientUserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PreUpdateEmailEntity_UserEntityId",
                table: "PreUpdateEmailEntity",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenEntity_UserEntityId",
                table: "RefreshTokenEntity",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntity_ClientEntityId",
                table: "ServiceEntity",
                column: "ClientEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_UserTypeEntityId",
                table: "UserEntity",
                column: "UserTypeEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressEntity");

            migrationBuilder.DropTable(
                name: "PreRegistrationEntity");

            migrationBuilder.DropTable(
                name: "PreUpdateEmailEntity");

            migrationBuilder.DropTable(
                name: "RefreshTokenEntity");

            migrationBuilder.DropTable(
                name: "ServiceEntity");

            migrationBuilder.DropTable(
                name: "CityEntity");

            migrationBuilder.DropTable(
                name: "StateEntity");

            migrationBuilder.DropTable(
                name: "ClientEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "UserTypeEntity");
        }
    }
}
