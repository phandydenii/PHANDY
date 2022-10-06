namespace SCHOOL_MANAGEMENT_SYSTEM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        create_date = c.DateTime(nullable: false),
                        create_by = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}
