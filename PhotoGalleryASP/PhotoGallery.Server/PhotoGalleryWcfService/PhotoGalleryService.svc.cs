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
        private IUserSrv userSrv;
        private IPhotoSrv photoSrv;

        public PhotoGalleryService(IUserSrv userSrv,IPhotoSrv photoSrv)
        {
            this.userSrv = userSrv;
            this.photoSrv = photoSrv;
        }

        #region UserService
        public void CreateUser(UserRegisterDto user)
        {
            try
            {
                userSrv.CreateUser(user);
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
                return userSrv.AuthenticateUser(login, password);
            }
            catch (MissingDataException ex)
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
            return userSrv.FindUserByUserLogin(login);
        }

        public UserInfoDto GetUserPublicInfo(string login)
        {
            try {
                return userSrv.GetUserPublicInfo(login);
            }
            catch (MissingDataException ex)
            {
                throw new FaultException<ServiceDataError>(new ServiceDataError(ex.Message));
            }
        }

        public UserDto UpdateUserEmail(int userId, string newEmail)
        {
            try
            {
                return userSrv.UpdateUserEmail(userId, newEmail);
            }
            catch (UserDataException ex){
                throw new FaultException<ServiceValidationError>(new ServiceValidationError(ex.Message));
            }
        }

        public UserDto UpdateUserPassword(int userId, string currentPassword, string newPassword)
        {
            try
            {
               return userSrv.UpdateUserPassword(userId, currentPassword, newPassword);
            }
            catch (UserDataException ex)
            {
                throw new FaultException<ServiceValidationError>(new ServiceValidationError(ex.Message));
            }
        }

        public UserDto UpdateUserInfo(int userId,string firstName,string secondName)
        {           
              return userSrv.UpdateUserInfo(userId,firstName,secondName);                             
        }

        #endregion

        #region PhotoService
        public void AddPhoto(PhotoDto photoDto) {
            photoSrv.AddPhoto(photoDto);
        }               

        public PhotoInfoDto GetPhotoInfo(int photoId)
        {
            try
            {
                return photoSrv.GetPhotoInfo(photoId);
            }
            catch (MissingDataException ex)
            {
                throw new FaultException<ServiceDataError>(new ServiceDataError(ex.Message));
            }       
        }

        public void UpdatePhotoInfo(int photoId, string photoName)
        {
            try
            {
                photoSrv.UpdatePhotoInfo(photoId, photoName);
            }
            catch (MissingDataException ex)
            {
                throw new FaultException<ServiceDataError>(new ServiceDataError(ex.Message));
            }
        }

        public void DeletePhoto(int photoId)
        {
            try
            {
                photoSrv.DeletePhoto(photoId);
            }
            catch (MissingDataException ex)
            {
                throw new FaultException<ServiceDataError>(new ServiceDataError(ex.Message));
            }
        }

        public PhotoDto[] GetPhotos(int userId, int? albumOwnerId, int page, int pageSize)
        {
            return photoSrv.GetPhotos(userId, albumOwnerId, page, pageSize);
        }


        public PhotoDto[] GetUserAlbum(int userId, int page, int pageSize)
        {
            return photoSrv.GetUserAlbum(userId, page, pageSize);
        }
              
        public void SetPhotoRating(int photoId, int userId, int rating)
        {
            photoSrv.SetPhotoRating(photoId, userId,rating);
        }

        #endregion

    }
}
