using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Favorites
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public string UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
