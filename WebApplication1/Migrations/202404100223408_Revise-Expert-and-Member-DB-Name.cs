namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviseExpertandMemberDBName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Masters", newName: "Master");
            RenameTable(name: "dbo.Members", newName: "Member");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Member", newName: "Members");
            RenameTable(name: "dbo.Master", newName: "Masters");
        }
    }
}
