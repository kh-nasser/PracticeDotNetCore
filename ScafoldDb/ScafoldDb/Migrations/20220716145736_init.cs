using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScafoldDb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notify");

            migrationBuilder.CreateTable(
                name: "tblAccount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblNotifyEventType",
                schema: "notify",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNotifyEventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblNotifyEvent",
                schema: "notify",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_tblAccount_tblNotifyEvent = table.Column<long>(type: "bigint", nullable: false),
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FK_tblNotifyEventType_tblNotifyEvent = table.Column<byte>(type: "tinyint", nullable: false),
                    FK_tblAccount_tblNotifyEvent_Reciever = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNotifyEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblNotifyEvent_tblAccount",
                        column: x => x.FK_tblAccount_tblNotifyEvent,
                        principalTable: "tblAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblNotifyEvent_tblAccount1",
                        column: x => x.FK_tblAccount_tblNotifyEvent_Reciever,
                        principalTable: "tblAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblNotifyEvent_tblNotifyEventType",
                        column: x => x.FK_tblNotifyEventType_tblNotifyEvent,
                        principalSchema: "notify",
                        principalTable: "tblNotifyEventType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_batchId",
                schema: "notify",
                table: "tblNotifyEvent",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNotifyEvent_FK_tblAccount_tblNotifyEvent",
                schema: "notify",
                table: "tblNotifyEvent",
                column: "FK_tblAccount_tblNotifyEvent");

            migrationBuilder.CreateIndex(
                name: "IX_tblNotifyEvent_FK_tblAccount_tblNotifyEvent_Reciever",
                schema: "notify",
                table: "tblNotifyEvent",
                column: "FK_tblAccount_tblNotifyEvent_Reciever");

            migrationBuilder.CreateIndex(
                name: "IX_tblNotifyEvent_FK_tblNotifyEventType_tblNotifyEvent",
                schema: "notify",
                table: "tblNotifyEvent",
                column: "FK_tblNotifyEventType_tblNotifyEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblNotifyEvent",
                schema: "notify");

            migrationBuilder.DropTable(
                name: "tblAccount");

            migrationBuilder.DropTable(
                name: "tblNotifyEventType",
                schema: "notify");
        }
    }
}
