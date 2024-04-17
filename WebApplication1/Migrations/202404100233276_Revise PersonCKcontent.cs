namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevisePersonCKcontent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Master", "PersonCkContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Master", "PersonCkContent", c => c.String(nullable: false));
        }
    }
}
