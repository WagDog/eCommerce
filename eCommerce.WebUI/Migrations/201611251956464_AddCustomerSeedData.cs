namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerSeedData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers (CustomerName, Address1, Address2, Town, PostCode, " + 
                                        "IsSubscribedToNewsletter, MembershipTypeId, BirthDate) VALUES (" +
                                        "'Paul Wagstaff', '15 Any Street', 'Brondby', 'Chelsea', " + 
                                        "'SK10 1AA', 0, 1, '1966-03-02')");
            Sql("INSERT INTO Customers (CustomerName, Address1, Address2, Town, PostCode, " +
                                        "IsSubscribedToNewsletter, MembershipTypeId, BirthDate) VALUES (" +
                                        "'Rosanna Wagstaff', '15 Any Street', 'Brondby', 'Chelsea', " +
                                        "'SK10 1AA', 1, 2, '1966-01-19')");
        }

        public override void Down()
        {
        }
    }
}
