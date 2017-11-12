namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DId = c.Int(nullable: false, identity: true),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.DId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(nullable: false),
                        PassWord = c.String(nullable: false),
                        Email = c.String(),
                        OfficePhone = c.String(),
                        MobilePhone = c.String(),
                        Pic = c.Binary(),
                        DId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DId)
                .Index(t => t.DId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        T_title = c.String(),
                        UId = c.Int(),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Users", t => t.UId)
                .Index(t => t.UId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Item_id = c.String(nullable: false, maxLength: 128),
                        Item_owner = c.String(),
                        Item_kind = c.String(),
                    })
                .PrimaryKey(t => t.Item_id);
            
            CreateTable(
                "dbo.Items_history",
                c => new
                    {
                        Item_id = c.String(nullable: false, maxLength: 128),
                        Item_owner = c.String(),
                        Item_kind = c.String(),
                        Datetime_taken = c.DateTime(),
                        Datetime_return = c.DateTime(),
                    })
                .PrimaryKey(t => t.Item_id);
            
            CreateTable(
                "dbo.Laptops",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Location_history",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        T_title = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members_history",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        T_member = c.String(),
                        T_pin = c.String(),
                        T_title = c.String(),
                        T_identity = c.String(),
                        Datetime_enter = c.DateTime(),
                        Datetime_leave = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpareParts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_members",
                c => new
                    {
                        T_member = c.String(nullable: false, maxLength: 128),
                        T_pin = c.String(),
                        T_title = c.String(),
                        T_identity = c.String(),
                    })
                .PrimaryKey(t => t.T_member);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Pin = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Creator = c.String(),
                        Visibility = c.String(),
                    })
                .PrimaryKey(t => t.Pin);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Kind = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "UId", "dbo.Users");
            DropForeignKey("dbo.Users", "DId", "dbo.Departments");
            DropIndex("dbo.Locations", new[] { "UId" });
            DropIndex("dbo.Users", new[] { "DId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Teams");
            DropTable("dbo.T_members");
            DropTable("dbo.SpareParts");
            DropTable("dbo.Members_history");
            DropTable("dbo.Location_history");
            DropTable("dbo.Laptops");
            DropTable("dbo.Items_history");
            DropTable("dbo.Items");
            DropTable("dbo.Locations");
            DropTable("dbo.Users");
            DropTable("dbo.Departments");
        }
    }
}
