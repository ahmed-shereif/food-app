using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
  public  class Recipe : BaseModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string Category { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}
