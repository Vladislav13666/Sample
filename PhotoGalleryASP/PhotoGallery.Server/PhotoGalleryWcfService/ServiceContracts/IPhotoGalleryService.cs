using PhotoGallery.Dto;
using PhotoGalleryWcfService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGalleryWcfService.ServiceContracts
{
        [ServiceContract]
        public interface IPhotoGalleryService
        {
        #region UserService

        [OperationContract]
        [FaultContract(typeof(ServiceValidationError))]
        void CreateUser(UserRegisterDto user);     

        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        [FaultContract(typeof(ServiceValidationError))]
        UserDto AuthenticateUser(string login, string password);

        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        UserDto FindUserByUserLogin(string login);

        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        UserInfoDto GetUserPublicInfo(string login);

        [OperationContract]
        [FaultContract(typeof(ServiceValidationError))]
        [FaultContract(typeof(ServiceDataError))]
        UserDto UpdateUserEmail(int userId, string newEmail);

        [OperationContract]
        [FaultContract(typeof(ServiceValidationError))]
        [FaultContract(typeof(ServiceDataError))]
        void UpdateUserPassword(int userId, string currentPassword, string newPassword);


        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        void UpdateUserInfo(int userId,string firstName,string secondName);

        #endregion

        #region PhotoGalleryService

        [OperationContract]
        void AddPhoto(PhotoDto photoDto);              

        [OperationContract]
        PhotoDto[] GetPhotos(int userObserverId, int? userId, int page, int pageSize);

        [OperationContract]
        PhotoDto[] GetUserAlbum(int userId, int page, int pageSize);


        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        PhotoInfoDto GetPhotoInfo(int photoId);

        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        void UpdatePhotoInfo(int photoId, string photoName);

        [OperationContract]
        [FaultContract(typeof(ServiceDataError))]
        void DeletePhoto(int photoId);      

        [OperationContract]
        void SetPhotoRating(int photoId, int userId, int rating);

        #endregion
    }
}
