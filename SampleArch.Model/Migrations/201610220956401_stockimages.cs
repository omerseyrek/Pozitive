namespace SampleArch.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockimages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        StockImageLarge = c.Binary(nullable: false),
                        StockImageSmall = c.Binary(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.StockId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockImage", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockImage", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.StockImage", "CreateUserId", "dbo.Users");
            DropIndex("dbo.StockImage", new[] { "UpdateUserId" });
            DropIndex("dbo.StockImage", new[] { "CreateUserId" });
            DropIndex("dbo.StockImage", new[] { "StockId" });
            DropTable("dbo.StockImage");
        }
    }
}
