namespace SampleArch.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nonrequiredFirm : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "TaxNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Companies", "TaxOffice", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "TaxOffice", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Companies", "TaxNumber", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
