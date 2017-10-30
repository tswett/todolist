namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoItemDeadlines : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoItems", "DaysRequired", c => c.Double(nullable: false));
            AddColumn("dbo.ToDoItems", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoItems", "Deadline");
            DropColumn("dbo.ToDoItems", "DaysRequired");
        }
    }
}
