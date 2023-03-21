using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Course_Planner_Felix_Berinde.Models
{
    public class Widget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GadgetId { get; set; } //Foreign key from the Gadget class/table
        public string Name { get; set; }
        public string Color { get; set; }
        public int InStock { get; set; }
        public decimal Price { get; set; }
        public bool StartNotification { get; set; }
        public DateTime CreationDate { get; set; }
        public string Notes { get; set; }
        
    }
}
