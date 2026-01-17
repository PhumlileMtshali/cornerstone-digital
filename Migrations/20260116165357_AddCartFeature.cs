using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CornerstoneDigital.Migrations
{
    /// <inheritdoc />
    public partial class AddCartFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomizationNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeliveryTime", "Description" },
                values: new object[] { new DateTime(2026, 1, 16, 18, 53, 53, 637, DateTimeKind.Local).AddTicks(5740), "2-4 weeks", "Custom web solutions built with modern technologies. From e-commerce platforms to corporate websites, we deliver scalable and responsive applications." });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 1, 16, 18, 53, 53, 637, DateTimeKind.Local).AddTicks(5785), "Beautiful, intuitive interfaces that enhance user experience and drive engagement. User-centered design that converts visitors into customers." });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DeliveryTime", "Description", "Name", "Price" },
                values: new object[] { new DateTime(2026, 1, 16, 18, 53, 53, 637, DateTimeKind.Local).AddTicks(5787), "3-5 weeks", "Complete online store setup with payment integration, inventory management, and secure checkout. Build your digital storefront with confidence.", "E-Commerce Solutions", 20000.00m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DeliveryTime", "Description", "Name", "Price" },
                values: new object[] { new DateTime(2026, 1, 16, 18, 53, 53, 637, DateTimeKind.Local).AddTicks(5788), "4-6 weeks", "Native and cross-platform mobile applications for iOS and Android. Deliver seamless experiences on any device your customers use.", "Mobile App Development", 25000.00m });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CreatedAt", "DeliveryTime", "Description", "Features", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 5, new DateTime(2026, 1, 16, 18, 53, 53, 637, DateTimeKind.Local).AddTicks(5790), "Ongoing", "SEO optimization, social media strategy, and content marketing to boost your online presence and drive traffic to your digital platforms.", null, true, "Digital Marketing", 6000.00m },
                    { 6, new DateTime(2026, 1, 16, 18, 53, 53, 637, DateTimeKind.Local).AddTicks(5791), "2-3 weeks", "Complete brand identity packages including logo design, color palette, typography, and brand guidelines to establish your unique presence.", null, true, "Brand Identity Design", 10000.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ServiceId",
                table: "CartItems",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeliveryTime", "Description" },
                values: new object[] { new DateTime(2026, 1, 14, 12, 8, 24, 678, DateTimeKind.Local).AddTicks(4610), "2-3 weeks", "Custom web solutions built with modern technologies tailored to your business needs." });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2026, 1, 14, 12, 8, 24, 678, DateTimeKind.Local).AddTicks(4646), "Beautiful, intuitive interfaces that enhance user experience and drive engagement." });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DeliveryTime", "Description", "Name", "Price" },
                values: new object[] { new DateTime(2026, 1, 14, 12, 8, 24, 678, DateTimeKind.Local).AddTicks(4649), "1 week", "Showcase your work with stunning portfolio websites that leave lasting impressions.", "Portfolio Creation", 5000.00m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DeliveryTime", "Description", "Name", "Price" },
                values: new object[] { new DateTime(2026, 1, 14, 12, 8, 24, 678, DateTimeKind.Local).AddTicks(4651), "Ongoing", "Strategic guidance to elevate your digital presence and achieve business goals.", "Digital Consulting", 3000.00m });
        }
    }
}
