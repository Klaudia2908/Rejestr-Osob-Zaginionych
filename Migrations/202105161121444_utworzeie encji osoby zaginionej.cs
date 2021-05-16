namespace RejOsZag.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class utworzeieencjiosobyzaginionej : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EncjaOsobyZaginionejs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Plec = c.String(),
                        MiejscowoscZaginiecia = c.String(),
                        DataUrodzenia = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EncjaOsobyZaginionejs");
        }
    }
}
