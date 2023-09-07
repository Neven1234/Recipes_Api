using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class NewRecipecs
    {
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public string Ingredients { get; set; }
        [Required]
        [MinLength(1)]
        public string Steps { get; set; }
        public string? Image { get; set; }
    }
}
