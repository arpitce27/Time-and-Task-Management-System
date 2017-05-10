namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkModelChanged551114 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Works", "CreationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Works", "Deadline", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Works", "Deadline", c => c.DateTime());
            AlterColumn("dbo.Works", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
