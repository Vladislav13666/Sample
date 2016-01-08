namespace PhotoGallery.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "PhotoOwner", c => c.String(nullable: false));
            AddColumn("dbo.Photos", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Photos", "NameImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "NameImage", c => c.String());
            DropColumn("dbo.Photos", "Title");
            DropColumn("dbo.Photos", "PhotoOwner");
        }
    }
}
