namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerBirthDateData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET BirthDate = '1966-03-02 00:00:00' WHERE CustomerId = 1");
            Sql("UPDATE Customers SET BirthDate = '1966-01-19 00:00:00' WHERE CustomerId = 2");
        }
        
        public override void Down()
        {
        }
    }
}
