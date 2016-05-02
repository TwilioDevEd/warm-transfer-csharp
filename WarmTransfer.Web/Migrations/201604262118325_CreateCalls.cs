using System.Data.Entity.Migrations;

namespace WarmTransfer.Web.Migrations
{
    public partial class CreateCalls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentId = c.String(),
                        ConferenceId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Calls");
        }
    }
}
