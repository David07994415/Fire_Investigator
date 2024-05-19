namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPremissionDBURLFieldandUpdateUpdateTimeColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permission", "URL", c => c.String(maxLength: 400));
            AlterColumn("dbo.Business", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.BusinessCategory", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Master", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Member", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Member", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Master", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.BusinessCategory", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Business", "UpdateTime", c => c.DateTime());
            DropColumn("dbo.Permission", "URL");
        }
    }
}
