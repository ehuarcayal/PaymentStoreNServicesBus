using FluentMigrator;

namespace Transactions.Infrastructure.Migrations.MySQL
{
    [Migration(3)]
    public class AlterTableOutBox : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("3_AlterTableOutBox.sql");
        }

        public override void Down()
        {
        }
    }
}
