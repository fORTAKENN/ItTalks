﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItTalks.Data.Migrations
{
    public partial class PostsIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpploadData",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpploadData",
                table: "Posts");
        }
    }
}
