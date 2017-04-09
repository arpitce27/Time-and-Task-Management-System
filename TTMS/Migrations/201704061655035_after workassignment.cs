namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class afterworkassignment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkAssigneds", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.WorkAssigneds", new[] { "UserID" });
            CreateTable(
                "dbo.WorkAssignedApplicationUsers",
                c => new
                    {
                        WorkAssigned_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.WorkAssigned_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.WorkAssigneds", t => t.WorkAssigned_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.WorkAssigned_ID)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "UserRoles_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "UserRoles_Id");
            AddForeignKey("dbo.AspNetUsers", "UserRoles_Id", "dbo.AspNetRoles", "Id");
            DropColumn("dbo.WorkAssigneds", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkAssigneds", "UserID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.WorkAssignedApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkAssignedApplicationUsers", "WorkAssigned_ID", "dbo.WorkAssigneds");
            DropForeignKey("dbo.AspNetUsers", "UserRoles_Id", "dbo.AspNetRoles");
            DropIndex("dbo.WorkAssignedApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.WorkAssignedApplicationUsers", new[] { "WorkAssigned_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "UserRoles_Id" });
            DropColumn("dbo.AspNetUsers", "UserRoles_Id");
            DropTable("dbo.WorkAssignedApplicationUsers");
            CreateIndex("dbo.WorkAssigneds", "UserID");
            AddForeignKey("dbo.WorkAssigneds", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
