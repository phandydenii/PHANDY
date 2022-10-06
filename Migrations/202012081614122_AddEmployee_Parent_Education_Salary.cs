namespace SCHOOL_MANAGEMENT_SYSTEM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployee_Parent_Education_Salary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employee_id = c.Int(nullable: false),
                        edulcation_level = c.String(),
                        skill = c.String(),
                        from_year = c.DateTime(nullable: false),
                        to_year = c.DateTime(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        create_by = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.employee_id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        marital_Status = c.Boolean(nullable: false),
                        name = c.String(nullable: false),
                        name_kh = c.String(nullable: false),
                        gender = c.String(),
                        phone = c.String(),
                        email = c.String(),
                        emp_address = c.String(),
                        img = c.String(),
                        dob = c.DateTime(nullable: false),
                        pob = c.String(),
                        is_active = c.Boolean(nullable: false),
                        create_by = c.String(),
                        create_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.BranchId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employee_id = c.Int(nullable: false),
                        work_location = c.String(),
                        position = c.String(),
                        from_year = c.DateTime(nullable: false),
                        to_year = c.DateTime(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        create_by = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.employee_id);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employee_id = c.Int(nullable: false),
                        name = c.String(maxLength: 100),
                        name_kh = c.String(maxLength: 100),
                        gender = c.String(),
                        Note = c.String(maxLength: 255),
                        parent_job = c.String(),
                        parent_address = c.String(maxLength: 255),
                        create_date = c.DateTime(nullable: false),
                        create_by = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.employee_id);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employee_id = c.Int(nullable: false),
                        amount = c.Double(nullable: false),
                        create_date = c.DateTime(nullable: false),
                        create_by = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.employee_id, cascadeDelete: true)
                .Index(t => t.employee_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salaries", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Parents", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Experiences", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Educations", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "BranchId", "dbo.Branches");
            DropIndex("dbo.Salaries", new[] { "employee_id" });
            DropIndex("dbo.Parents", new[] { "employee_id" });
            DropIndex("dbo.Experiences", new[] { "employee_id" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "BranchId" });
            DropIndex("dbo.Educations", new[] { "employee_id" });
            DropTable("dbo.Salaries");
            DropTable("dbo.Parents");
            DropTable("dbo.Experiences");
            DropTable("dbo.Employees");
            DropTable("dbo.Educations");
        }
    }
}
