using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auth",
                columns: table => new
                {
                    user_email = table.Column<string>(type: "TEXT", nullable: false),
                    auth_password_hash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    auth_password_salt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    auth_role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth", x => x.user_email);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    post_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    post_title = table.Column<string>(type: "TEXT", nullable: false),
                    post_content = table.Column<string>(type: "TEXT", nullable: false),
                    post_creation_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    post_update_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.post_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_nick = table.Column<string>(type: "TEXT", nullable: false),
                    user_email = table.Column<string>(type: "TEXT", nullable: false),
                    user_ativo = table.Column<int>(type: "INTEGER", nullable: false),
                    user_creation_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
