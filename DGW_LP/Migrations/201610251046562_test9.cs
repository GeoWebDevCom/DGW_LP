namespace DGW_LP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "createdDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Videos", "createdDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "createdDate");
            DropColumn("dbo.Comments", "createdDate");
        }
    }
}
