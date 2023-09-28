using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class RateAndReview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        [MinLength(3)]
        public string Review { get; set; }

        public int RecipeId { get; set; }
        public string UserId { get; set; }
    }
}
