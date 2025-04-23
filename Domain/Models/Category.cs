using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Relationship
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
