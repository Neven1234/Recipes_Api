using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public string Ingredients { get; set; }
        [Required]
        [MinLength(1)]
        public string Steps { get; set; }
        public string? Image { get; set; }

        [ForeignKey("UserID")]
        public string UserName { get; set; }
        //navigation
       

    }
}
