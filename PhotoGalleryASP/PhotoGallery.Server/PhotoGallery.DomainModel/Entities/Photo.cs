using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Entities
{
  public class Photo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; }
        [Column]
        [Required]
        public int UserId { get; set; }
        [Column]
        [Required]
        public string PhotoOwner { get; set; }
        [Column]
        [Required]
        public string ImagePath { get; set; }

        [Column]
        [Required]
        public string Title { get; set; }
        [Column]
        [Required]
        public DateTime CreateTime { get; set; }
        [Column]
        public int AllRating { get; set; }
        [Column]       
        public int AllVotes { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
