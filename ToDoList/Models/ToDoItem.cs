using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public ItemStatus Status { get; set; }

        public enum ItemStatus
        {
            Pending = 0,

            [Display(Name = "In progress")]
            InProgress = 2,

            Done = 1,
        }
    }

    public class ToDoItemDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}