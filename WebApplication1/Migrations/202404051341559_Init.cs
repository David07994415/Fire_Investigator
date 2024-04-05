namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Business",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.BusinessCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 400),
                        Password = c.String(nullable: false, maxLength: 400),
                        Salt = c.String(nullable: false, maxLength: 400),
                        NickName = c.String(nullable: false, maxLength: 100),
                        Guid = c.Guid(nullable: false),
                        Permission = c.String(maxLength: 200),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 400),
                        RecursiveId = c.Int(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permission", t => t.RecursiveId)
                .Index(t => t.RecursiveId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permission", "RecursiveId", "dbo.Permission");
            DropForeignKey("dbo.Business", "CategoryId", "dbo.BusinessCategory");
            DropIndex("dbo.Permission", new[] { "RecursiveId" });
            DropIndex("dbo.Business", new[] { "CategoryId" });
            DropTable("dbo.Permission");
            DropTable("dbo.Members");
            DropTable("dbo.BusinessCategory");
            DropTable("dbo.Business");
        }
    }
}
