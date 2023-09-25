using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartShop.Server.Migrations
{
    /// <inheritdoc />
    public partial class Productseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "The is a test for the first book", "https://th.bing.com/th/id/R.6d1e757f6ae90bd14e92c28f53507b68?rik=wU%2f%2bO0hTLOuLLA&riu=http%3a%2f%2fsteve-lovelace.com%2fwordpress%2fwp-content%2fuploads%2f2012%2f06%2fwikipedia-book-cover.png&ehk=gsHeCrgqoz9gCkutL0MeqjMwsMKHcVR3gZ1rwT4W7l4%3d&risl=&pid=ImgRaw&r=0", 4.66m, "The First Book" },
                    { 2, "Also the test for the second book is here", "https://th.bing.com/th/id/R.41432b68c640604fef2ca79e7415779f?rik=xisO17TB5K1yUg&riu=http%3a%2f%2fwww.fh-augsburg.de%2f%7eharsch%2fanglica%2fChronology%2f20thC%2fKipling%2fkip_juti.jpg&ehk=BhTwvGJSZUMT1wEPyG0ZPYntyh7%2fIYpFIH4E5LLRWi0%3d&risl=&pid=ImgRaw&r=0", 8.66m, "The second Book" },
                    { 3, "Third book will have the test here ", "https://th.bing.com/th/id/OIP._ZquNBZkvqxJZTt7srYcwAHaJY?pid=ImgDet&rs=1", 6.66m, "The third Book" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
