namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWebContentUpdateTimeColumnToNonComputed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WebContent", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WebContent", "UpdateTime", c => c.DateTime());
        }
    }
}
