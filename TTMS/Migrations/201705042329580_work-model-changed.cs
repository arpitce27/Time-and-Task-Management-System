namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workmodelchanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Works", "WorkTitle", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Works", "WorkDescr", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Works", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Works", "Deadline", c => c.DateTime());
            AlterColumn("dbo.Works", "WorkDescr", c => c.String(maxLength: 400));
            AlterColumn("dbo.Works", "WorkTitle", c => c.String(maxLength: 200));
        }
    }
}
