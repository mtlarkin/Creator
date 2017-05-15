using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Creator.Migrations
{
    public partial class AddItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    Bump = table.Column<int>(nullable: false),
                    CommentOwnerId = table.Column<string>(nullable: true),
                    Knock = table.Column<int>(nullable: false),
                    ParentCommentCommentId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_CommentOwnerId",
                        column: x => x.CommentOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentCommentId",
                        column: x => x.ParentCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    Bumps = table.Column<int>(nullable: false),
                    Knocks = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    PostCommentCommentId = table.Column<int>(nullable: true),
                    PostOwnerId = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Comments_PostCommentCommentId",
                        column: x => x.PostCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_PostOwnerId",
                        column: x => x.PostOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentOwnerId",
                table: "Comments",
                column: "CommentOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentCommentId",
                table: "Comments",
                column: "ParentCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostCommentCommentId",
                table: "Posts",
                column: "PostCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostOwnerId",
                table: "Posts",
                column: "PostOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
