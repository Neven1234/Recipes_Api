using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class IngredientsList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
