namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeatureDirectroyDBSheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 400),
                        RecursiveId = c.Int(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directory", t => t.RecursiveId)
                .Index(t => t.RecursiveId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Directory", "RecursiveId", "dbo.Directory");
            DropIndex("dbo.Directory", new[] { "RecursiveId" });
            DropTable("dbo.Directory");
        }
    }
}
