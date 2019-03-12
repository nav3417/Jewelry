namespace JewelryDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JewelryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JewelryColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JewelryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Priority = c.Int(nullable: false),
                        Url = c.String(),
                        Jewelry_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jewelries", t => t.Jewelry_Id)
                .Index(t => t.Jewelry_Id);
            
            CreateTable(
                "dbo.Jewelries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(),
                        Category_Id = c.Int(),
                        Color_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JewelryCategories", t => t.Category_Id)
                .ForeignKey("dbo.JewelryColors", t => t.Color_Id)
                .ForeignKey("dbo.JewelryTypes", t => t.Type_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Color_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.JewelryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Comments_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Comments_Id)
                .Index(t => t.Comments_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginId = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        ImageUrl = c.String(),
                        BirthDate = c.DateTime(),
                        FullName = c.String(),
                        SecurityQuestion = c.String(),
                        SecurityAnswer = c.String(),
                        City_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        jewelry_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jewelries", t => t.jewelry_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.jewelry_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Orderdate = c.DateTime(nullable: false),
                        OrderHolder = c.String(),
                        Cellno = c.String(),
                        TotalAmount = c.Int(nullable: false),
                        city_Id = c.Int(),
                        status_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.city_Id)
                .ForeignKey("dbo.Status", t => t.status_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.city_Id)
                .Index(t => t.status_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Orders", "status_Id", "dbo.Status");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "city_Id", "dbo.Cities");
            DropForeignKey("dbo.OrderDetails", "jewelry_Id", "dbo.Jewelries");
            DropForeignKey("dbo.Comments", "Comments_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Jewelries", "Type_Id", "dbo.JewelryTypes");
            DropForeignKey("dbo.JewelryImages", "Jewelry_Id", "dbo.Jewelries");
            DropForeignKey("dbo.Jewelries", "Color_Id", "dbo.JewelryColors");
            DropForeignKey("dbo.Jewelries", "Category_Id", "dbo.JewelryCategories");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "status_Id" });
            DropIndex("dbo.Orders", new[] { "city_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.OrderDetails", new[] { "jewelry_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "City_Id" });
            DropIndex("dbo.Comments", new[] { "Comments_Id" });
            DropIndex("dbo.Jewelries", new[] { "Type_Id" });
            DropIndex("dbo.Jewelries", new[] { "Color_Id" });
            DropIndex("dbo.Jewelries", new[] { "Category_Id" });
            DropIndex("dbo.JewelryImages", new[] { "Jewelry_Id" });
            DropTable("dbo.Status");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.JewelryTypes");
            DropTable("dbo.Jewelries");
            DropTable("dbo.JewelryImages");
            DropTable("dbo.JewelryColors");
            DropTable("dbo.Cities");
            DropTable("dbo.JewelryCategories");
        }
    }
}
