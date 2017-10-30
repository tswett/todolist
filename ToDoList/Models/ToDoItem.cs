using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public int ID { get; set; }
        public string Content { get; set; }
    }

    public class ToDoItemDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}