namespace AshtonBro.Code.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBandType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        BandId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.BandId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Bands", t => t.BandId, cascadeDelete: true)
                .Index(t => t.BandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "BandId", "dbo.Bands");
            DropIndex("dbo.Songs", new[] { "BandId" });
            DropTable("dbo.Songs");
            DropTable("dbo.Bands");
        }
    }
}
