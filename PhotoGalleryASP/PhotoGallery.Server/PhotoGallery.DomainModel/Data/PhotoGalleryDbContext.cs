using PhotoGallery.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Data
{
   public class PhotoGalleryDbContext: DbContext
    {
      public PhotoGalleryDbContext() :base(ConfigurationManager.ConnectionStrings["DefaultConnect"].ConnectionString) { }
            //:base(@"Data Source=.\SQLEXPRESS;Initial Catalog=PhotoGallery;Integrated Security=True;Integrated Security=True")          
               
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
           {
         
            modelBuilder.Entity<User>().HasMany(e => e.Photos).WithRequired(e => e.User).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(e => e.UserRatings).WithRequired(e => e.User).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Photo>().HasMany(e => e.UserRatings).WithRequired(e => e.Photo).HasForeignKey(e => e.PhotoId).WillCascadeOnDelete(false);
     
        }
      

        public User GetUserByUserLogin(string login)
        {
            return Users.FirstOrDefault(u => u.Login == login);          
        }

        public User GetUserByUserId(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public Photo GetPhotoByPhotoId(int id) {
            return Photos.FirstOrDefault(u => u.PhotoId == id);
        }

        public IQueryable<Photo> GetPhotos(int? userId) {
            IQueryable<Photo> photos = Photos;
            if (userId != null) {
                photos = photos.Where(u=>u.UserId==userId);
            }
            return photos;
        }

        public IQueryable<UserRating> GetRatingsByUserId(int userId) {
            return UserRatings.Where(u => u.UserId == userId);
        }

        public UserRating GetUserPhotoRating(int userId,int photoId) {
            return UserRatings.Where(u => u.UserId == userId).Where(u=>u.PhotoId==photoId).FirstOrDefault();
        }
    }
}
