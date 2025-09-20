using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } 

        public Category() { }
        public Category(string Name, string? Description ) 
        {
            this.Name = Name;
            this.Description = Description;
            this.IsActive = true;
        }  
    }
}
