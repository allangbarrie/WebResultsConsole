using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdminConsole.Migrations
{
    /// <inheritdoc />
    public partial class MyCommand2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Captain",
                columns: table => new
                {
                    CaptainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captain", x => x.CaptainId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cutoff = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.StageId);
                });

            migrationBuilder.CreateTable(
                name: "TeamCategory",
                columns: table => new
                {
                    TeamCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCategory", x => x.TeamCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Record_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Record_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "StageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CaptainId = table.Column<int>(type: "int", nullable: false),
                    TeamCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Team_Captain_CaptainId",
                        column: x => x.CaptainId,
                        principalTable: "Captain",
                        principalColumn: "CaptainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_TeamCategory_TeamCategoryId",
                        column: x => x.TeamCategoryId,
                        principalTable: "TeamCategory",
                        principalColumn: "TeamCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BibNumber",
                columns: table => new
                {
                    BibNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibNumber", x => x.BibNumberId);
                    table.ForeignKey(
                        name: "FK_BibNumber_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderBoard",
                columns: table => new
                {
                    LeaderBoardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Difference = table.Column<TimeSpan>(type: "time", nullable: false),
                    TeamCategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryPosition = table.Column<int>(type: "int", nullable: false),
                    CategoryDifference = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderBoard", x => x.LeaderBoardId);
                    table.ForeignKey(
                        name: "FK_LeaderBoard_TeamCategory_TeamCategoryId",
                        column: x => x.TeamCategoryId,
                        principalTable: "TeamCategory",
                        principalColumn: "TeamCategoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LeaderBoard_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    BibNumberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Result_BibNumber_BibNumberId",
                        column: x => x.BibNumberId,
                        principalTable: "BibNumber",
                        principalColumn: "BibNumberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "StageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Runner",
                columns: table => new
                {
                    RunnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BibNumberId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runner", x => x.RunnerId);
                    table.ForeignKey(
                        name: "FK_Runner_BibNumber_BibNumberId",
                        column: x => x.BibNumberId,
                        principalTable: "BibNumber",
                        principalColumn: "BibNumberId");
                    table.ForeignKey(
                        name: "FK_Runner_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Runner_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BibNumber_TeamId",
                table: "BibNumber",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderBoard_TeamCategoryId",
                table: "LeaderBoard",
                column: "TeamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderBoard_TeamId",
                table: "LeaderBoard",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_CategoryId",
                table: "Record",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_StageId",
                table: "Record",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_BibNumberId",
                table: "Result",
                column: "BibNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_StageId",
                table: "Result",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Runner_BibNumberId",
                table: "Runner",
                column: "BibNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Runner_CategoryId",
                table: "Runner",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Runner_TeamId",
                table: "Runner",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_CaptainId",
                table: "Team",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamCategoryId",
                table: "Team",
                column: "TeamCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaderBoard");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Runner");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "BibNumber");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Captain");

            migrationBuilder.DropTable(
                name: "TeamCategory");
        }
    }
}
