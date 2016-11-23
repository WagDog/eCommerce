namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipTypeSeedData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)VALUES(0,0,0)");
            Sql("INSERT INTO MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)VALUES(10,3,5)");
            Sql("INSERT INTO MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)VALUES(20,6,10)");
            Sql("INSERT INTO MembershipTypes (SignUpFee, DurationInMonths, DiscountRate)VALUES(40,12,15)");
        }
        
        public override void Down()
        {
        }
    }
}
