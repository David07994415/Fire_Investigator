namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKnowledgeDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Knowledge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        FileName = c.String(nullable: false),
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
            DropTable("dbo.Knowledge");
        }
    }
}
