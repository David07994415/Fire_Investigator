namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberDBNewColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "UpdateUser", c => c.Int());
            AddColumn("dbo.Member", "CreateUser", c => c.Int());
            AddColumn("dbo.Member", "IsApproved", c => c.Boolean());
            AddColumn("dbo.Member", "IdCat", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "IdCat");
            DropColumn("dbo.Member", "IsApproved");
            DropColumn("dbo.Member", "CreateUser");
            DropColumn("dbo.Member", "UpdateUser");
        }
    }
}
