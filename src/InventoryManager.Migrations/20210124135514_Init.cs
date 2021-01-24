using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManager.src.InventoryManager.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Login);
                    table.ForeignKey(
                        name: "FK_User_Group_UserGroupID",
                        column: x => x.UserGroupID,
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Техник" });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Администратор" });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Суперпользователь" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Login", "FirstName", "LastName", "MiddleName", "Password", "UserGroupID" },
                values: new object[] { "root", "Иван", "Иванов", "Иванович", "root", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Group_Name",
                table: "Group",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserGroupID",
                table: "User",
                column: "UserGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
