using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeParts.Data.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: false),
                    nickname = table.Column<string>(type: "character varying", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    role = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.login);
                });

            migrationBuilder.CreateTable(
                name: "content",
                columns: table => new
                {
                    pk_code_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_account_login = table.Column<string>(type: "character varying", nullable: false),
                    content = table.Column<string>(type: "character varying", nullable: false),
                    tags = table.Column<string>(type: "character varying", nullable: false),
                    title = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content", x => x.pk_code_id);
                    table.ForeignKey(
                        name: "FK_content_accounts_fk_account_login",
                        column: x => x.fk_account_login,
                        principalTable: "accounts",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_login",
                table: "accounts",
                column: "login");

            migrationBuilder.CreateIndex(
                name: "IX_content_fk_account_login",
                table: "content",
                column: "fk_account_login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content");

            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
