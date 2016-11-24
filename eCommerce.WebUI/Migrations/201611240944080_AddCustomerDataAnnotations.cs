namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Address1", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Address1", c => c.String());
            AlterColumn("dbo.Customers", "CustomerName", c => c.String());
        }
    }
}
