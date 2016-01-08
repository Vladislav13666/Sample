using PhotoGallery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel
{
  public interface IPhotoGallerySrv
    {
        void CreateUser(UserRegisterDto user);
        UserDto AuthenticateUser(string login,string password);
        UserDto FindUserByUserLogin(string login);

        void UpdateUserInfo(int userId, string firstName, string secondName);
        UserDto UpdateUserEmail(int userId, string newEmail);
        void UpdateUserPassword(int userId, string currentPassword, string newPassword);

        void AddPhoto(PhotoDto photoDto);

        PhotoDto[] GetPhotosByUserId(int userId, int page, int pageSize);

        PhotoDto[] GetPhotos(int page, int pageSize);

        PhotoInfoDto GetPhotoInfo(int photoId);

        void UpdatePhotoInfo(int photoId, string photoName);

        void DeletePhoto(int photoId);

        int GetPhotoCount(int? userId);
        /* void AddPhotoByUserLogin(string login, PhotoDto photoDto);
        List<PhotoDto> FindUserPhotosByLogin(string login, int page, int pageSize);
        PhotoDto FindPhotobyId(int? id);
        void DeletePhoto(int? id);
        void EditPhoto(PhotoDto photoDto);
        List<PhotoDto> GetAllPhotos(string login, string sortOrder, string userOwner, int page, int pageSize);
        int GetAllPhotosCountByUser(string login);
        void SetPhotoRating(int photoId, int currentUserRating, string loginUser);
        */
    }
}
