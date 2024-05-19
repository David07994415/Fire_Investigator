namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBulletinandMessageDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bulletin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Theme = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                        BulletinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bulletin", t => t.BulletinId, cascadeDelete: true)
                .Index(t => t.BulletinId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "BulletinId", "dbo.Bulletin");
            DropIndex("dbo.Message", new[] { "BulletinId" });
            DropTable("dbo.Message");
            DropTable("dbo.Bulletin");
        }
    }
}
