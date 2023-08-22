using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game.Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameForum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_NAME = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATED_BY = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    MODIFIED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BIRTHDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATED_BY = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    MODIFIED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUserIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QUESTIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Title = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Content = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUESTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Question_User",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateTable(
                name: "ANSWERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Content = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsBestAnswer = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANSWERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ANSWERS_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CATEGORIES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ANSWERS_QUESTIONS_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ANSWERS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateTable(
                name: "CategoryQuestion",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryQuestion", x => new { x.CategoriesId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_CategoryQuestion_CATEGORIES_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "CATEGORIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryQuestion_QUESTIONS_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAVORITE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAVORITE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Favorite_Question",
                        column: x => x.QuestionId,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Favorite_User",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateTable(
                name: "QuestionView",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionView", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionView_Question",
                        column: x => x.QuestionId,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QuestionView_User",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IS_DELETED = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Voted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vote_Question",
                        column: x => x.QuestionId,
                        principalTable: "QUESTIONS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Vote_User",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "USER_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ANSWERS_CategoryId",
                table: "ANSWERS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ANSWERS_QuestionId",
                table: "ANSWERS",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ANSWERS_UserId",
                table: "ANSWERS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryQuestion_QuestionsId",
                table: "CategoryQuestion",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITE_QuestionId",
                table: "FAVORITE",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITE_UserId",
                table: "FAVORITE",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QUESTIONS_UserId",
                table: "QUESTIONS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionView_QuestionId",
                table: "QuestionView",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionView_UserId",
                table: "QuestionView",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_QuestionId",
                table: "Vote",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_UserId",
                table: "Vote",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ANSWERS");

            migrationBuilder.DropTable(
                name: "CategoryQuestion");

            migrationBuilder.DropTable(
                name: "FAVORITE");

            migrationBuilder.DropTable(
                name: "QuestionView");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "QUESTIONS");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
