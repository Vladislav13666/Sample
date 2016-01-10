using PhotoGalleryWcfService.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PhotoGallery.Dto;
using PhotoGallery.DomainModel;
using PhotoGalleryWcfService.Exceptions;
using PhotoGallery.DomainModel.Exceptions;

namespace PhotoGalleryWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PhotoGalleryService : IPhotoGalleryService
    {
        private IPhotoGallerySrv photoGallerySrv;

        public PhotoGalleryService(IPhotoGallerySrv photoGallerySrv)
        {
            this.photoGallerySrv = photoGallerySrv;
        }
        public void CreateUser(UserRegisterDto user)
        {
            try
            {
                photoGallerySrv.CreateUser(user);
            }
            catch(UserDataException ex)
            {               
                throw new FaultException<ServiceValidationError>(new ServiceValidationError(ex.Message));
            }
            
        }
        
        public UserDto AuthenticateUser(string login, string password)
        {
            try
            {
                return photoGallerySrv.AuthenticateUser(login, password);
            }
            catch (UserNotFoundException ex)
            {               
                throw new FaultException<ServiceDataError>(new ServiceDataError(ex.Message));
            }
            catch (UserDataException ex)
            {
                throw new FaultException<ServiceValidationError>(new ServiceValidationError(ex.Message));
            }
        }

        public UserDto FindUserByUserLogin(string login)
        {
            return photoGallerySrv.FindUserByUserLogin(login);
        }

        public UserDto UpdateUserEmail(int userId, string newEmail)
        {
          return photoGallerySrv.UpdateUserEmail(userId, newEmail);
        }

        public void UpdateUserPassword(int userId, string currentPassword, string newPassword)
        {
            photoGallerySrv.UpdateUserPassword(userId, currentPassword,newPassword);
        }

        public void UpdateUserInfo(int userId,string firstName,string secondName)
        {
            try
            {
                photoGallerySrv.UpdateUserInfo(userId,firstName,secondName);
            }
           
            catch (ArgumentException ex)
            {
                ServiceDataError srvError = new ServiceDataError(ex.Message);
                throw new FaultException<ServiceDataError>(srvError);
            }        
           
        }
        public void AddPhoto(PhotoDto photoDto) {
            photoGallerySrv.AddPhoto(photoDto);
        }               

        public PhotoDto[] GetPhotos(int userObserverId, int? userId, int page, int pageSize)
        {
            return photoGallerySrv.GetPhotos(userObserverId, userId, page, pageSize);
        }


        public PhotoDto[] GetUserAlbum(int userId, int page, int pageSize)
        {
            return photoGallerySrv.GetUserAlbum(userId,page, pageSize);
        }

        public PhotoInfoDto GetPhotoInfo(int photoId)
        {
            return photoGallerySrv.GetPhotoInfo(photoId);
        }

        public void UpdatePhotoInfo(int photoId, string photoName)
        {
            photoGallerySrv.UpdatePhotoInfo(photoId, photoName);
        }

        public void DeletePhoto(int photoId)
        {
            photoGallerySrv.DeletePhoto(photoId);
        }

        public int GetPhotoCount(int? userId)
        {
            return photoGallerySrv.GetPhotoCount(userId);
        }
        public void SetPhotoRating(int photoId, int userId, int rating)
        {
            photoGallerySrv.SetPhotoRating(photoId, userId,rating);
        }

    }
}
