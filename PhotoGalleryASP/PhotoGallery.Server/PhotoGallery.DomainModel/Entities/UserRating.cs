using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Entities
{
  public class UserRating
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        [Key, Column(Order = 2)]
        public int PhotoId { get; set; }
        [Column]
        [Required]
        public int Rating { get; set; }
        public virtual User User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
