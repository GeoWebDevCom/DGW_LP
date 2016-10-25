namespace DGW_LP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "ThumbImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "ThumbImg");
        }
    }
}
