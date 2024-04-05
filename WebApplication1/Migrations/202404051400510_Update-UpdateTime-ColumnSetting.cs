namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUpdateTimeColumnSetting : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Business", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.BusinessCategory", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Members", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.BusinessCategory", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Business", "UpdateTime", c => c.DateTime());
        }
    }
}
