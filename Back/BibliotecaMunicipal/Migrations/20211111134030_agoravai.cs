using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaMunicipal.Migrations
{
    public partial class agoravai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Emprestado",
                table: "Emprestimo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emprestado",
                table: "Emprestimo");
        }
    }
}
