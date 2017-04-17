namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _41117worklogadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkId = c.Int(nullable: false),
                        CretionDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        TimeSpend = c.Double(nullable: false),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: true)
                .Index(t => t.WorkId)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkLogs", "WorkId", "dbo.Works");
            DropForeignKey("dbo.WorkLogs", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WorkLogs", new[] { "user_Id" });
            DropIndex("dbo.WorkLogs", new[] { "WorkId" });
            DropTable("dbo.WorkLogs");
        }
    }
}
