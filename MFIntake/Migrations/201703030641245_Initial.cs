namespace MFIntake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        agentFlag = c.Boolean(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        phoneNumber = c.String(),
                        AgencyName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Custodian",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustodianFlag = c.Boolean(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        phoneNumber = c.String(),
                        AgencyName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Exam",
                c => new
                    {
                        ExamID = c.Int(nullable: false, identity: true),
                        IntakeID = c.Int(nullable: false),
                        ExamType = c.String(),
                        ExamStatus = c.String(),
                        Analyst = c.String(),
                        ExamDate = c.DateTime(nullable: false),
                        ExamFileLocation = c.String(),
                        AddlEquipNeeded = c.Boolean(nullable: false),
                        ExamNote = c.String(maxLength: 250),
                        Persons_ID = c.Int(),
                        Statuses_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ExamID)
                .ForeignKey("dbo.Intake", t => t.IntakeID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.Persons_ID)
                .ForeignKey("dbo.Status", t => t.Statuses_ID)
                .Index(t => t.IntakeID)
                .Index(t => t.Persons_ID)
                .Index(t => t.Statuses_ID);
            
            CreateTable(
                "dbo.Intake",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CaseNumber = c.String(),
                        DeviceModel = c.String(),
                        FullName = c.String(),
                        Custodian = c.String(),
                        WarrantNumber = c.String(),
                        StorageLocation = c.String(nullable: false),
                        IntakeStatus = c.String(),
                        ReceivedDate = c.DateTime(nullable: false),
                        RequestedByDate = c.DateTime(nullable: false),
                        IntakeNote = c.String(maxLength: 250),
                        Agents_ID = c.Int(),
                        Custodians_ID = c.Int(),
                        Statuses_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.Agents_ID)
                .ForeignKey("dbo.Custodian", t => t.Custodians_ID)
                .ForeignKey("dbo.Status", t => t.Statuses_ID)
                .Index(t => t.Agents_ID)
                .Index(t => t.Custodians_ID)
                .Index(t => t.Statuses_ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusType = c.String(),
                        StatusName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        discriminator = c.String(),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        phoneNumber = c.String(),
                        AgencyName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exam", "Statuses_ID", "dbo.Status");
            DropForeignKey("dbo.Exam", "Persons_ID", "dbo.Person");
            DropForeignKey("dbo.Intake", "Statuses_ID", "dbo.Status");
            DropForeignKey("dbo.Exam", "IntakeID", "dbo.Intake");
            DropForeignKey("dbo.Intake", "Custodians_ID", "dbo.Custodian");
            DropForeignKey("dbo.Intake", "Agents_ID", "dbo.Agent");
            DropIndex("dbo.Intake", new[] { "Statuses_ID" });
            DropIndex("dbo.Intake", new[] { "Custodians_ID" });
            DropIndex("dbo.Intake", new[] { "Agents_ID" });
            DropIndex("dbo.Exam", new[] { "Statuses_ID" });
            DropIndex("dbo.Exam", new[] { "Persons_ID" });
            DropIndex("dbo.Exam", new[] { "IntakeID" });
            DropTable("dbo.Person");
            DropTable("dbo.Status");
            DropTable("dbo.Intake");
            DropTable("dbo.Exam");
            DropTable("dbo.Custodian");
            DropTable("dbo.Agent");
        }
    }
}
