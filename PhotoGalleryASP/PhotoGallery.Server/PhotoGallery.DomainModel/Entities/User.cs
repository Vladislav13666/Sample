using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Entities
{
   public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column]
        [Required]
        [StringLength(20)]
        [Index("IX_Login", IsUnique = true)]
        public string Login { get; set; }
       
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20)]
        [Index("IX_Email", IsUnique = true)]
        public string Email { get; set; }
        [Column]
        [Required]
        public byte[] Key { get; set; }

        [Column]
        [Required]
        public byte[] Salt { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
