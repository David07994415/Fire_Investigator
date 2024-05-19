namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMasterDBAddIsShowField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Master", "IsShow", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Master", "IsShow");
        }
    }
}
