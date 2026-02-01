using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDTOModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserRole",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserRole",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatedId",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Role",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Role",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatedId",
                table: "Role",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatedId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserCreatedId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserCreatedId",
                table: "UserRole",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserCreatedId",
                table: "Role",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserCreatedId",
                table: "Product",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserCreatedId",
                table: "Category",
                column: "UserCreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_UserCreatedId",
                table: "Category",
                column: "UserCreatedId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserCreatedId",
                table: "Product",
                column: "UserCreatedId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_User_UserCreatedId",
                table: "Role",
                column: "UserCreatedId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserCreatedId",
                table: "UserRole",
                column: "UserCreatedId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_UserCreatedId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserCreatedId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_User_UserCreatedId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserCreatedId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserCreatedId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_Role_UserCreatedId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Product_UserCreatedId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Category_UserCreatedId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Category");
        }
    }
}
