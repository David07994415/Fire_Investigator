namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectoryDBIsAuthMenuColumnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directory", "IsAuthMenu", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Directory", "IsAuthMenu", c => c.Boolean());
        }
    }
}
