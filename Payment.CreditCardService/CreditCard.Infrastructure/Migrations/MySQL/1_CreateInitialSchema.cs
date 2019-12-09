using FluentMigrator;

namespace CreditCards.Infrastructure.Migrations.MySQL
{
    public class CreateInitialSchema : Migration
    {
        public override void Down()
        {
            Execute.EmbeddedScript("1_CreateInitialSchema.sql");
        }

        public override void Up()
        {
            
        }
    }
}
