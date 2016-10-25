namespace DGW_LP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Video_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Videos", t => t.Video_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Video_Id);
            
            AddColumn("dbo.Videos", "Description", c => c.String());
            AddColumn("dbo.Videos", "AuthorName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Video_Id" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Videos", "AuthorName");
            DropColumn("dbo.Videos", "Description");
            DropTable("dbo.Comments");
        }
    }
}
