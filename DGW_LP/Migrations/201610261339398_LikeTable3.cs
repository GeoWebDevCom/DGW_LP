namespace DGW_LP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeTable3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        createdDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Video_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Videos", t => t.Video_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Video_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.Likes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "Video_Id" });
            DropIndex("dbo.Likes", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Likes");
        }
    }
}
