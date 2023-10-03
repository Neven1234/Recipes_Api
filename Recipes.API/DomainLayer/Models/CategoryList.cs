using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class CategoryList
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
