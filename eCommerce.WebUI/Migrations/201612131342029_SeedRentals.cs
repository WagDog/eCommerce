namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRentals : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Rentals (Customer_CustomerId, Movie_MovieId, DateRented)VALUES(1,3,'2016-12-01 00:00:00')");
            Sql("INSERT INTO Rentals (Customer_CustomerId, Movie_MovieId, DateRented)VALUES(2,2,'2016-12-01 00:00:00')");
            Sql("INSERT INTO Rentals (Customer_CustomerId, Movie_MovieId, DateRented)VALUES(3,1,'2016-12-01 00:00:00')");
        }
        
        public override void Down()
        {
            Sql("TRUNCATE TABLE Rentals");
        }
    }
}
