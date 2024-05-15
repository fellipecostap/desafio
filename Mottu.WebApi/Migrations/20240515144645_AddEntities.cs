using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptTermsDate",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "GenreType",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "UserNickName",
                table: "UserEntity");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "ServiceEntity");

            migrationBuilder.RenameColumn(
                name: "UserPhone",
                table: "UserEntity",
                newName: "CNHPhoto");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "ServiceEntity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlanEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Plan = table.Column<int>(type: "integer", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Fine = table.Column<double>(type: "double precision", nullable: false),
                    Created = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntity_PlanId",
                table: "ServiceEntity",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntity_PlanEntity_PlanId",
                table: "ServiceEntity",
                column: "PlanId",
                principalTable: "PlanEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntity_PlanEntity_PlanId",
                table: "ServiceEntity");

            migrationBuilder.DropTable(
                name: "PlanEntity");

            migrationBuilder.DropIndex(
                name: "IX_ServiceEntity_PlanId",
                table: "ServiceEntity");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "ServiceEntity");

            migrationBuilder.RenameColumn(
                name: "CNHPhoto",
                table: "UserEntity",
                newName: "UserPhone");

            migrationBuilder.AddColumn<string>(
                name: "AcceptTermsDate",
                table: "UserEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenreType",
                table: "UserEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserNickName",
                table: "UserEntity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Plan",
                table: "ServiceEntity",
                type: "integer",
                nullable: true);
        }
    }
}
