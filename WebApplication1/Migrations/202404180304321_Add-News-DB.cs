namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        PhotoPath = c.String(),
                        NewsCkContent = c.String(),
                        IsShow = c.Boolean(),
                        IsTop = c.Boolean(),
                        IssueTime = c.DateTime(),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
