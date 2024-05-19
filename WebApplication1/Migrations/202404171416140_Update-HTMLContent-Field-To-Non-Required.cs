namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHTMLContentFieldToNonRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WebContent", "HTMLContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WebContent", "HTMLContent", c => c.String(nullable: false));
        }
    }
}
