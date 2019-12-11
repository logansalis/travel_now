using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelNow.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsOfGuest_Users_CreatorID",
                table: "ReviewsOfGuest");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewsOfGuest_Listings_ListingID",
                table: "ReviewsOfGuest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewsOfGuest",
                table: "ReviewsOfGuest");

            migrationBuilder.RenameTable(
                name: "ReviewsOfGuest",
                newName: "ListingReviews");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewsOfGuest_ListingID",
                table: "ListingReviews",
                newName: "IX_ListingReviews_ListingID");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewsOfGuest_CreatorID",
                table: "ListingReviews",
                newName: "IX_ListingReviews_CreatorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListingReviews",
                table: "ListingReviews",
                column: "ReviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingReviews_Users_CreatorID",
                table: "ListingReviews",
                column: "CreatorID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListingReviews_Listings_ListingID",
                table: "ListingReviews",
                column: "ListingID",
                principalTable: "Listings",
                principalColumn: "ListingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingReviews_Users_CreatorID",
                table: "ListingReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingReviews_Listings_ListingID",
                table: "ListingReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListingReviews",
                table: "ListingReviews");

            migrationBuilder.RenameTable(
                name: "ListingReviews",
                newName: "ReviewsOfGuest");

            migrationBuilder.RenameIndex(
                name: "IX_ListingReviews_ListingID",
                table: "ReviewsOfGuest",
                newName: "IX_ReviewsOfGuest_ListingID");

            migrationBuilder.RenameIndex(
                name: "IX_ListingReviews_CreatorID",
                table: "ReviewsOfGuest",
                newName: "IX_ReviewsOfGuest_CreatorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewsOfGuest",
                table: "ReviewsOfGuest",
                column: "ReviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsOfGuest_Users_CreatorID",
                table: "ReviewsOfGuest",
                column: "CreatorID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewsOfGuest_Listings_ListingID",
                table: "ReviewsOfGuest",
                column: "ListingID",
                principalTable: "Listings",
                principalColumn: "ListingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
