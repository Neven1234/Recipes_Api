using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserToFollowId { get; set; }
        public string UserFollowerId { get; set; }
        public string UserName { get; set; }

    }
}
