namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoItemStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoItems", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoItems", "Status");
        }
    }
}
