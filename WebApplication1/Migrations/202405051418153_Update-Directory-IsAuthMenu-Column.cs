﻿namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDirectoryIsAuthMenuColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directory", "IsAuthMenu", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directory", "IsAuthMenu");
        }
    }
}
