using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMLearningWords.AccessToData.Migrations
{
    public partial class FirsPushDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StageOfMethods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageOfMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordsInEnglish",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ExeptionInWord = table.Column<bool>(nullable: false),
                    RightWord = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    StageOfMethodId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsInEnglish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordsInEnglish_StageOfMethods_StageOfMethodId",
                        column: x => x.StageOfMethodId,
                        principalTable: "StageOfMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslationOfWords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ExeptionInWord = table.Column<bool>(nullable: false),
                    WordInEnglishId = table.Column<long>(nullable: false),
                    RightTranslation = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    MustBeRemove = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationOfWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationOfWords_WordsInEnglish_WordInEnglishId",
                        column: x => x.WordInEnglishId,
                        principalTable: "WordsInEnglish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationOfWords_WordInEnglishId",
                table: "TranslationOfWords",
                column: "WordInEnglishId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsInEnglish_Name",
                table: "WordsInEnglish",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordsInEnglish_StageOfMethodId",
                table: "WordsInEnglish",
                column: "StageOfMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationOfWords");

            migrationBuilder.DropTable(
                name: "WordsInEnglish");

            migrationBuilder.DropTable(
                name: "StageOfMethods");
        }
    }
}
