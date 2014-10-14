namespace Biz4CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banners", "Description", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Banners", "Description", c => c.String(maxLength: 200));
        }
    }
}
