namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpertPageDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Masters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Ocupation = c.String(nullable: false, maxLength: 500),
                        PhotoPath = c.String(),
                        PersonCkContent = c.String(nullable: false),
                        UpdateUser = c.Int(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreateUser = c.Int(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Masters");
        }
    }
}
