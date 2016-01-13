using AutoMapper;
using PhotoGallery.DomainModel.Data;
using PhotoGallery.DomainModel.Entities;
using PhotoGallery.DomainModel.Exceptions;
using PhotoGallery.DomainModel.Security;
using PhotoGallery.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Impl
{
    public class PhotoGallerySrv : IPhotoGallerySrv
    {
        private PhotoGalleryDbContext db;
        public PhotoGallerySrv(PhotoGalleryDbContext context)
        {
            db = context;
        }


        #region UserService    
        public void CreateUser(UserRegisterDto user)
        {
            if (db.Users.FirstOrDefault(u => u.Login == user.Login) == null &&
                db.Users.FirstOrDefault(u => u.Email == user.Email) == null)
            {
                var newUser = Mapper.Map<UserRegisterDto, User>(user);
                AuthorizationHelper.GenerateKey(user.Password, newUser);
                db.Users.Add(newUser);
                db.SaveChanges();              
            }
            else
            {
                throw new UserDataException("user with such data already exists");
            }                  
        }

        public UserDto AuthenticateUser(string login,string password)
        {
            var user = db.GetUserByUserLogin(login);
            if (user == null)
            {
                throw new MissingDataException(login);
            }               
            if (AuthorizationHelper.CheckPassword(password, user))
            {
                return Mapper.Map<User, UserDto>(user);
            }
            throw new UserDataException(login);

        }

        public UserDto FindUserByUserLogin(string login)
        {
            var user=db.GetUserByUserLogin(login);
            if (user == null)
            {
                throw new MissingDataException(login);
            }               
            return Mapper.Map<User, UserDto>(user);
        }
             

        public UserDto UpdateUserEmail(int userId, string newEmail)
        {
            var oldUser = db.GetUserByUserId(userId);
            if (oldUser == null)
            {
                throw new MissingDataException("user not found");
            }

            if (db.Users.FirstOrDefault(u => u.Email == newEmail) == null)
            {                              
                oldUser.Email = newEmail;
                db.Entry(oldUser).State = EntityState.Modified;
                db.SaveChanges();
                return Mapper.Map<User, UserDto>(oldUser);
            }
            else
            {
                throw new UserDataException(newEmail);
            }
             
        }

        public void UpdateUserPassword(int userId,string currentPassword,string newPassword)
        {
               var oldUser = db.GetUserByUserId(userId);
                if (oldUser == null)
                {
                    throw new MissingDataException("user not found");
                }
            if (AuthorizationHelper.CheckPassword(currentPassword, oldUser))
            {
                AuthorizationHelper.GenerateKey(newPassword,oldUser);
                db.Entry(oldUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new UserDataException(currentPassword);
            }         
                      
        }

        public void UpdateUserInfo(int userId,string firstName,string secondName)
        {             
           var oldUser = db.GetUserByUserId(userId);
            if (oldUser == null)
            {
              throw new MissingDataException("user not found");
            }
            oldUser.FirstName = firstName;
            oldUser.LastName = secondName;
            db.Entry(oldUser).State = EntityState.Modified;
            db.SaveChanges();
        }

        #endregion

        #region PhotoGalleryService
        public void AddPhoto(PhotoDto photoDto)
        {
           var photo=Mapper.Map<PhotoDto, Photo>(photoDto);
            db.Photos.Add(photo);
            db.SaveChanges();
        }

        public PhotoInfoDto GetPhotoInfo(int photoId)
        {
          var photo= db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photo != null)
            {
               return Mapper.Map<Photo, PhotoInfoDto> (photo);
            }

            throw new MissingDataException();
        }

        public void UpdatePhotoInfo(int photoId, string photoName)
        {
            var photo = db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photo != null)
            {
                photo.Title = photoName;
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
            }
            else {
                throw new MissingDataException();
            }

        }

        public void DeletePhoto(int photoId)
        {
            var photo = db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photo == null)
            {
                throw new MissingDataException();
            }
            db.UserRatings.RemoveRange(db.UserRatings.Where(u => u.PhotoId == photoId));           
            db.Photos.Remove(photo);
            db.SaveChanges();


        }

        public PhotoDto[] GetPhotos(int userObserverId, int? userId, int page, int pageSize)
        {
            IQueryable<Photo> photos = db.Photos;
            if (userId != null)
            {
                photos = photos.Where(u => u.UserId == userId);
            }

            photos = photos.OrderBy(u => u.PhotoId).Skip(page * pageSize).Take(pageSize);
            var ratings = db.UserRatings.Where(u => u.UserId == userObserverId);
            var photoScore = from p in photos
                             join r in ratings
                                on p.PhotoId equals r.PhotoId
                                into pr
                             from b in pr.DefaultIfEmpty()
                             select new PhotoDto
                             {
                                 PhotoId = p.PhotoId,
                                 UserId = p.UserId,
                                 AverageRating = p.AllVotes == 0 ? 0 : p.AllRating / p.AllVotes,
                                 CreateTime = p.CreateTime,
                                 ImagePath = p.ImagePath,
                                 PhotoOwner = p.PhotoOwner,
                                 CurrentUserRating = (b == null ? 0 : b.Rating),
                                 Title = p.Title
                             };

            return photoScore.ToArray();

        }

        public PhotoDto[] GetUserAlbum(int userId, int page, int pageSize)
        {
            var photos = db.Photos.Where(u => u.UserId == userId).OrderBy(u => u.PhotoId).Skip(page  * pageSize)
                .Take(pageSize).ToArray();
            return Mapper.Map<Photo[], PhotoDto[]>(photos);
        }
                   

        public void SetPhotoRating(int photoId, int userId, int rating)
        {
            var photo = db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            var userPhotoRating = db.UserRatings.Where(p => p.PhotoId == photoId).Where(p => p.UserId == userId).FirstOrDefault();
            if (userPhotoRating == null)
            {
                db.UserRatings.Add(new UserRating { UserId = userId, PhotoId = photoId, Rating = rating });
                photo.AllVotes++;
                photo.AllRating += rating;
                db.SaveChanges();
            }
            else
            {
                photo.AllRating += rating - userPhotoRating.Rating;
                userPhotoRating.Rating = rating;
                db.Entry(userPhotoRating).State = EntityState.Modified;
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion
    }
}
       


