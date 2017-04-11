namespace TTMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _41017commentsrecommentingdeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Comment_ID", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Comment_ID" });
            DropColumn("dbo.Comments", "ParentCommentId");
            DropColumn("dbo.Comments", "Comment_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Comment_ID", c => c.Int());
            AddColumn("dbo.Comments", "ParentCommentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Comment_ID");
            AddForeignKey("dbo.Comments", "Comment_ID", "dbo.Comments", "ID");
        }
    }
}
