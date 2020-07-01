namespace AshtonBro.Code.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBandCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bands", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bands", "Country");
        }
    }
}
