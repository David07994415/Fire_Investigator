namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWebContentDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebContent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HTMLContent = c.String(nullable: false),
                        DirectroyId = c.Int(nullable: false),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directory", t => t.DirectroyId, cascadeDelete: true)
                .Index(t => t.DirectroyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebContent", "DirectroyId", "dbo.Directory");
            DropIndex("dbo.WebContent", new[] { "DirectroyId" });
            DropTable("dbo.WebContent");
        }
    }
}
