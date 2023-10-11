using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ShoppingList
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("userId")]
        public string UserId { get; set; }

        [Required]
        public string Ingredient { get; set; }
        public bool Purchased { get; set; }
    }
}
