namespace eCommerce.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'968414c6-d53f-443c-8ccf-92979a2bb32a', N'paulwagstaff66@gmail.com', 0, N'AJ4iui610HBEsGqyhyaXMHXj/dnWldauzKOZUAYQu1iEAhrdXE94HKPb0quo4W4ZEA==', N'4cc3abc2-2f25-40f1-8abd-7d0afe97cfc6', NULL, 0, 0, NULL, 1, 0, N'paulwagstaff66@gmail.com')
                  INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'a6339003-edcd-4357-b9e1-590421ab60e3', N'sales@gomobilewebsites.com', 0, N'AJL9IhfY7iIwzocMSd2sUjlqyhXlqYx15CNyaMhMAvPRdFxxT9sYbFRF1OLsLtDYdQ==', N'222c55db-1599-4ec7-bf73-86dc4546499a', NULL, 0, 0, NULL, 1, 0, N'sales@gomobilewebsites.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2854e080-39d9-407c-9e1e-80d00578948f', N'canManage')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'968414c6-d53f-443c-8ccf-92979a2bb32a', N'2854e080-39d9-407c-9e1e-80d00578948f')
                  ");
        }

    public override void Down()
        {
        }
    }
}
