using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetitionWebApi.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPropertyNameInPieceOwnedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Piece_Description",
                table: "Performances",
                newName: "Piece_Composer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Piece_Composer",
                table: "Performances",
                newName: "Piece_Description");
        }
    }
}
