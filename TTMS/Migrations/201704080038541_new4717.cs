namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new4717 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkAssignedApplicationUsers", "WorkAssigned_ID", "dbo.WorkAssigneds");
            DropForeignKey("dbo.WorkAssignedApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkAssigneds", "WorkID", "dbo.Works");
            DropIndex("dbo.WorkAssigneds", new[] { "WorkID" });
            DropIndex("dbo.WorkAssignedApplicationUsers", new[] { "WorkAssigned_ID" });
            DropIndex("dbo.WorkAssignedApplicationUsers", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.WorkAssignments",
                c => new
                    {
                        Work_ID = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Work_ID, t.User_Id })
                .ForeignKey("dbo.Works", t => t.Work_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Work_ID)
                .Index(t => t.User_Id);
            
            DropTable("dbo.WorkAssigneds");
            DropTable("dbo.WorkAssignedApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WorkAssignedApplicationUsers",
                c => new
                    {
                        WorkAssigned_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.WorkAssigned_ID, t.ApplicationUser_Id });
            
            CreateTable(
                "dbo.WorkAssigneds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkID = c.Int(nullable: false),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.WorkUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkUsers", "Work_ID", "dbo.Works");
            DropIndex("dbo.WorkUsers", new[] { "User_Id" });
            DropIndex("dbo.WorkUsers", new[] { "Work_ID" });
            DropTable("dbo.WorkUsers");
            CreateIndex("dbo.WorkAssignedApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.WorkAssignedApplicationUsers", "WorkAssigned_ID");
            CreateIndex("dbo.WorkAssigneds", "WorkID");
            AddForeignKey("dbo.WorkAssigneds", "WorkID", "dbo.Works", "ID", cascadeDelete: true);
            AddForeignKey("dbo.WorkAssignedApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkAssignedApplicationUsers", "WorkAssigned_ID", "dbo.WorkAssigneds", "ID", cascadeDelete: true);
        }
    }
}
