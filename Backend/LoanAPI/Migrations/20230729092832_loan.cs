using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanAPI.Migrations
{
    public partial class loan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AccountNumberSequence",
                startValue: 20089080L);

            migrationBuilder.CreateTable(
                name: "LoansApplied",
                columns: table => new
                {
                    LoanAppliedID = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR AccountNumberSequence"),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanAmount = table.Column<double>(type: "float", nullable: false),
                    AmountToPay = table.Column<double>(type: "float", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<double>(type: "float", nullable: false),
                    LoanStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoansApplied", x => x.LoanAppliedID);
                });

            migrationBuilder.CreateTable(
                name: "LoansOffered",
                columns: table => new
                {
                    LoanOfferedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interest = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoansOffered", x => x.LoanOfferedID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoansApplied");

            migrationBuilder.DropTable(
                name: "LoansOffered");

            migrationBuilder.DropSequence(
                name: "AccountNumberSequence");
        }
    }
}
