namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexDataSheetintoDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndexCover",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoverName = c.String(nullable: false, maxLength: 100),
                        PhotoPath = c.String(),
                        IsShow = c.Boolean(),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IndexLink",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkName = c.String(nullable: false, maxLength: 100),
                        LinkPath = c.String(nullable: false),
                        PhotoPath = c.String(),
                        IsShow = c.Boolean(),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IndexPurpose",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HTMLContent = c.String(),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IndexPurpose");
            DropTable("dbo.IndexLink");
            DropTable("dbo.IndexCover");
        }
    }
}
