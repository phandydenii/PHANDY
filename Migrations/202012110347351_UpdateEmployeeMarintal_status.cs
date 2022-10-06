namespace SCHOOL_MANAGEMENT_SYSTEM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeMarintal_status : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "marital_Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "marital_Status", c => c.Boolean(nullable: false));
        }
    }
}
