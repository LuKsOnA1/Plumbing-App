using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class addClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserClaims",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7642babd-0925-498c-9745-8dfc20bbabba",
                column: "ConcurrencyStamp",
                value: "827d1e46-8790-40ca-81cb-5961cf1327cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9315c2d1-f8f6-488b-922f-729acafa5e24",
                column: "ConcurrencyStamp",
                value: "e4fe131f-cd6a-4c7e-be4f-30b758439e17");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Discriminator", "UserId" },
                values: new object[] { 1, "AdminObserverExpireDate", "01/03/2024", "AppUserClaim", "94a83b4b-0637-429d-b10d-cd973c283fac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26cb59ca-ea6e-4b82-9791-aee6bdd4c81d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30c66829-a582-48f7-9f0b-a63e858053d9", "AQAAAAIAAYagAAAAECGJoomHmckEwmQuOZHDMcgV31qFqljLK0prT30Ue9KNwrlpRDVROH874zRQ6NI1sQ==", "c9744c1c-5141-43aa-ba7c-8b2edf3831b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94a83b4b-0637-429d-b10d-cd973c283fac",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1504e28-49f3-4ddf-8538-f472f4bd1e51", "AQAAAAIAAYagAAAAEIaMDyoyQ7U/qH+Fc5BO2induF6HVLVwHARrKxIBDs/+s+9FL0OkMikx+xINRODgFA==", "dc9fdae1-f7f9-4217-b0bb-5ce156c10f23" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserClaims");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7642babd-0925-498c-9745-8dfc20bbabba",
                column: "ConcurrencyStamp",
                value: "d8ee0e89-3e58-4030-b748-5f305a5efdac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9315c2d1-f8f6-488b-922f-729acafa5e24",
                column: "ConcurrencyStamp",
                value: "35c7d105-49b5-4d1a-9a80-b3aa07ca9994");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26cb59ca-ea6e-4b82-9791-aee6bdd4c81d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "665b9685-104f-4963-b48f-02d7896ee2c1", "AQAAAAIAAYagAAAAED4PnOq5sDqXPCEDATau3TnGYScxlGEbxu3Yp8+Vpj211HPvfJTXenOBJ1stj9aEMQ==", "0a31434b-94b3-4ca7-b615-442303b2265d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94a83b4b-0637-429d-b10d-cd973c283fac",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec0308e7-57f8-4b8c-b34d-8ad04cd1dfb0", "AQAAAAIAAYagAAAAEL8zgEANjEEZxkVQkiZuJkBbafIG4UT1hpOzkMG23kYXUXPq2lUEFsl7LKWXeKzfxw==", "91ad785e-42b9-4925-8e61-f2c1ac126516" });
        }
    }
}
