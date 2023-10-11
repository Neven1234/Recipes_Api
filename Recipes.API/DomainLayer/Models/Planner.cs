using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Planner
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserID")]
        public string UserId { get; set; }

        [Required]
        public int RecipeId { get; set; }
        [Required]
        public DateTime Date { get; set;}
    }
}
