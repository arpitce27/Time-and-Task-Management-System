namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkModelChanged0505 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Works", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Works", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
