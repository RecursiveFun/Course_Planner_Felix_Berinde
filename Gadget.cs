using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Course_Planner_Felix_Berinde.Models
{
    public class Gadget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int InStock { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
