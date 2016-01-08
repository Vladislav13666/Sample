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
                throw new UserNotFoundException(login);
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
                throw new UserNotFoundException(login);
            }               
            return Mapper.Map<User, UserDto>(user);
        }
             

        public UserDto UpdateUserEmail(int userId, string newEmail)
        {
            var oldUser = db.GetUserByUserId(userId);
            if (oldUser == null)
            {
                throw new UserNotFoundException("user not found");
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
                    throw new UserNotFoundException("user not found");
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
              throw new UserNotFoundException("user not found");
            }
            oldUser.FirstName = firstName;
            oldUser.LastName = secondName;
            db.Entry(oldUser).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddPhoto(PhotoDto photoDto)
        {
           var photo=Mapper.Map<PhotoDto, Photo>(photoDto);
            db.Photos.Add(photo);
            db.SaveChanges();
        }

        public PhotoDto[] GetPhotosByUserId(int userId,int page,int pageSize)
        {           
            var photos = db.Photos.Where(u => u.UserId == userId).OrderBy(u => u.PhotoId).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return Mapper.Map<Photo[], PhotoDto[]>(photos);          
        }

        public PhotoInfoDto GetPhotoInfo(int photoId)
        {
          var photo= db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photo != null)
            {
               return Mapper.Map<Photo, PhotoInfoDto> (photo);
            }

            throw new NotImplementedException();
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

        }

        public void DeletePhoto(int photoId)
        {
            var photo = db.Photos.FirstOrDefault(p => p.PhotoId == photoId);
            if (photo == null)
            {
                throw new ArgumentNullException();
            }
            db.Photos.Remove(photo);
            db.SaveChanges();


        }

        public int GetPhotoCount(int? userId)
        {
            if (userId != null) {
                return db.Photos.Where(u => u.UserId == userId).Count();
            }
          return db.Photos.Count();
        }

        public PhotoDto[] GetPhotos(int page, int pageSize)
        {
            var photos = db.Photos.OrderBy(u => u.PhotoId).Skip((page - 1) * pageSize).Take(pageSize).ToArray();
            return Mapper.Map<Photo[], PhotoDto[]>(photos);
        }
    }
}
       

                    
/*
             

              

                public List<PhotoDto> FindUserPhotosByLogin(string login, int page, int pageSize)
                {
                    List<PhotoDto> photosDto = new List<PhotoDto>();
                    var userDto = FindUserByLogin(login);
                    if (userDto != null)
                    {
                        var photos = db.Photos.Where(u => u.UserId == userDto.Id).ToList();
                        foreach (var photo in photos)
                        {
                            photosDto.Add(new PhotoDto
                            {
                                PhotoId = photo.PhotoId,
                                NameImage = photo.NameImage,
                                ImagePath = photo.ImagePath,
                                AverageRating = photo.AllVotes == 0 ? 0 : (int)photo.AllRating / photo.AllVotes,
                                CreateTime = photo.CreateTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))
                            });
                        }
                        return photosDto.OrderBy(u => u.PhotoId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    }
                    return null;
                }

                public List<PhotoDto> GetAllPhotos(string login, string sortOrder, string userOwner, int page, int pageSize)
                {
                    //// в начале сортируем, потом делаем Skip и Take, потом маппим на List<PhotoDto>
                    ///ThenBy, убрать все навигационные свойста и Include

                    //var rating = from p in db.UserRatings
                    //             where p.UserId == userId
                    //             select u;

                    //IEnumerable<Photo> photos = from photo in db.Photos
                    //                            orderby photo.AllRatings descending, photo.PhotoId descending
                    //                            select photo;



                    //var photoScore = from bk in photos
                    //                 join ordr in rating
                    //                    on bk.PhotoId equals ordr.PhotoId
                    //                    into a
                    //                 from b in a.DefaultIfEmpty()
                    //                 select new PhotoDto
                    //                 {
                    //                     PhotoId = bk.PhotoId,
                    //                     Rating = (b == null ? 0 : b.UserRating)
                    //                     ///все остальные свойства PhotoDto
                    //                 };

                    //return photoScore;


                    /// так писать плохо
                    var Album = new List<PhotoDto>();
                    UserRating ratingCurUser = null;
                    var photos = db.Photos.Include(c => c.UserRatings).Include(c => c.User);
                    var userObserver = FindUserByLogin(login);
                    var photoAlbumOwner = FindUserByLogin(userOwner);
                    if (photoAlbumOwner != null)
                    {
                        photos = photos.Where(u => u.UserId == photoAlbumOwner.Id);
                    }

                    foreach (var m in photos)
                    {
                        if (userObserver != null)
                        {
                            ratingCurUser = m.UserRatings.Where(u => u.PhotoId == m.PhotoId).FirstOrDefault(u => u.UserId == userObserver.Id);
                        }

                        Album.Add(new PhotoDto
                        {
                            PhotoId = m.PhotoId,
                            UserId = m.UserId,
                            PhotoOwner = m.User.Login,
                            NameImage = m.NameImage,
                            ImagePath = m.ImagePath,
                            CurrentUserRating = ratingCurUser == null ? 0 : ratingCurUser.Rating,
                            AverageRating = m.AllVotes == 0 ? 0 : (int)m.AllRating / m.AllVotes,
                            CreateTime = m.CreateTime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))
                        });
                    }
                    switch (sortOrder)
                    {
                        case "rating":
                            Album = Album.OrderByDescending(u => u.PhotoId).ToList();
                            Album = Album.OrderByDescending(u => u.AverageRating).ToList();
                            break;
                        case "date":
                            Album = Album.OrderByDescending(u => u.PhotoId).ToList();
                            break;
                        default:
                            Album = Album.OrderBy(u => u.PhotoId).ToList();
                            break;
                    }
                    return Album.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }

                public int GetAllPhotosCountByUser(string login)
                {
                    var user = FindUserByLogin(login);
                    if (user != null)
                    {
                        return db.Photos.Where(u => u.UserId == user.Id).Count();
                    }
                    return db.Photos.Count();
                }

                public void SetPhotoRating(int photoId, int currentUserRating, string loginUser)
                {
                    var user = FindUserByLogin(loginUser);
                    Photo photo = db.Photos.Where(p => p.PhotoId == photoId).FirstOrDefault();
                    var userPhotoRating = db.UserRatings.Where(p => p.PhotoId == photo.PhotoId).Where(p => p.UserId == user.Id).FirstOrDefault();
                    if (userPhotoRating == null)
                    {
                        db.UserRatings.Add(new UserRating { UserId = user.Id, PhotoId = photoId, Rating = currentUserRating });
                        photo.AllVotes++;
                        photo.AllRating += currentUserRating;
                        db.SaveChanges();
                    }
                    else
                    {
                        photo.AllRating += currentUserRating - userPhotoRating.Rating;
                        userPhotoRating.Rating = currentUserRating;
                        db.Entry(userPhotoRating).State = EntityState.Modified;
                        db.Entry(photo).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }

                #endregion
            */
    

