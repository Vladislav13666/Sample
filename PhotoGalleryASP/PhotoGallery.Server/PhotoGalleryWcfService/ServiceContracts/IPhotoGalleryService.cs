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
        UserDto UpdateUserEmail(int userId, string newEmail);

        [OperationContract]
        void UpdateUserPassword(int userId, string currentPassword, string newPassword);


        [OperationContract]
        void UpdateUserInfo(int userId,string firstName,string secondName);

        [OperationContract]
        void AddPhoto(PhotoDto photoDto);

        [OperationContract]
        PhotoDto[] GetPhotosByUserId(int userId, int userObserverId, int page, int pageSize);

        [OperationContract]
        PhotoDto[] GetPhotos(int userObserverId, int page, int pageSize);

        [OperationContract]
        PhotoDto[] GetUserAlbum(int userId, int page, int pageSize);


        [OperationContract]
        PhotoInfoDto GetPhotoInfo(int photoId);

        [OperationContract]
        void UpdatePhotoInfo(int photoId, string photoName);

        [OperationContract]
        void DeletePhoto(int photoId);

        [OperationContract]
        int GetPhotoCount(int? userId);

        [OperationContract]
        void SetPhotoRating(int photoId, int userId, int rating);

         }
}
