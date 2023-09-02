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
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Steps { get; set; }
        public string ImageName { get; set; }
    }
}
