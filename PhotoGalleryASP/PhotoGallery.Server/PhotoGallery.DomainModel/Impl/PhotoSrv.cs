using AutoMapper;
using PhotoGallery.DomainModel.Data;
using PhotoGallery.DomainModel.Entities;
using PhotoGallery.DomainModel.Exceptions;
using PhotoGallery.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Impl
{
    public class PhotoSrv: IPhotoSrv
    {
        private PhotoGalleryDbContext db;
        public PhotoSrv(PhotoGalleryDbContext context)
        {
            db = context;
        }
        public void AddPhoto(PhotoDto photoDto)
        {
            var photo = Mapper.Map<PhotoDto, Photo>(photoDto);
            db.Photos.Add(photo);
            db.SaveChanges();
        }

        public PhotoInfoDto GetPhotoInfo(int photoId)
        {
            var photo = db.GetPhotoByPhotoId(photoId);
            if (photo == null)
            {
                throw new MissingDataException();
            }
            return Mapper.Map<Photo, PhotoInfoDto>(photo);

        }

        public void UpdatePhotoInfo(int photoId, string photoName)
        {
            var photo = db.GetPhotoByPhotoId(photoId);
            if (photo == null)
            {
                throw new MissingDataException();
            }
            photo.Title = photoName;
            db.Entry(photo).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeletePhoto(int photoId)
        {
            var photo = db.GetPhotoByPhotoId(photoId);
            if (photo == null)
            {
                throw new MissingDataException();
            }
            db.UserRatings.RemoveRange(db.UserRatings.Where(u => u.PhotoId == photoId));
            db.Photos.Remove(photo);
            db.SaveChanges();


        }

        public PhotoDto[] GetPhotos(int userId, int? albumOwnerId, int page, int pageSize)
        {
            var photos = db.GetPhotos(albumOwnerId);
            photos = photos.OrderByDescending(u => u.PhotoId).Skip(page * pageSize).Take(pageSize);
            var ratings = db.GetRatingsByUserId(userId);

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
            var photos = db.GetPhotos(userId);
            photos = photos.OrderByDescending(u => u.PhotoId).Skip(page * pageSize).Take(pageSize);
            return Mapper.Map<Photo[], PhotoDto[]>(photos.ToArray());
        }


        public void SetPhotoRating(int photoId, int userId, int rating)
        {
            var photo = db.GetPhotoByPhotoId(photoId);
            var userPhotoRating = db.GetUserPhotoRating(userId, photoId);
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
    }
}
