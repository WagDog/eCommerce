namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdColumnsToRentals : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "Customer_CustomerId", newName: "CustomerId");
            RenameColumn(table: "dbo.Rentals", name: "Movie_MovieId", newName: "MovieId");
            RenameIndex(table: "dbo.Rentals", name: "IX_Customer_CustomerId", newName: "IX_CustomerId");
            RenameIndex(table: "dbo.Rentals", name: "IX_Movie_MovieId", newName: "IX_MovieId");
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));
            Sql("UPDATE Movies SET NumberAvailable = NumbeInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
            RenameIndex(table: "dbo.Rentals", name: "IX_MovieId", newName: "IX_Movie_MovieId");
            RenameIndex(table: "dbo.Rentals", name: "IX_CustomerId", newName: "IX_Customer_CustomerId");
            RenameColumn(table: "dbo.Rentals", name: "MovieId", newName: "Movie_MovieId");
            RenameColumn(table: "dbo.Rentals", name: "CustomerId", newName: "Customer_CustomerId");
        }
    }
}
