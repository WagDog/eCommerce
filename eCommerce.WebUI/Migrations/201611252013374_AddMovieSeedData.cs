namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieSeedData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies ([Name], [GenreId], [ReleaseDate], " + 
                "[DateAdded], [NumberInStock]) VALUES " + 
                "('Die Hard', 2, '1989-02-03 00:00:00', '2016-01-01 00:00:00', 5)");
            Sql("INSERT INTO Movies ([Name], [GenreId], [ReleaseDate], " +
                "[DateAdded], [NumberInStock]) VALUES " +
                "('Airplane', 1, N'1980-08-29 00:00:00', N'2016-01-01 00:00:00', 10)");
            Sql("INSERT INTO Movies ([Name], [GenreId], [ReleaseDate], " +
                "[DateAdded], [NumberInStock]) VALUES " +
                "('Shrek', 3, N'2001-06-29 00:00:00', N'2016-01-01 00:00:00', 15)");
            Sql("INSERT INTO Movies ([Name], [GenreId], [ReleaseDate], " +
                "[DateAdded], [NumberInStock]) VALUES " +
                "('Gone with the Wind', 4, N'1940-01-17 00:00:00', N'2016-02-01 00:00:00', 2)");
        }

        public override void Down()
        {
        }
    }
}
