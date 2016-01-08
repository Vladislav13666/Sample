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

        PhotoDto[] GetPhotosByUserId(int userId, int userObserverId, int page, int pageSize);

        PhotoDto[] GetPhotos(int userObserverId, int page, int pageSize);

        PhotoDto[] GetUserAlbum(int userId, int page, int pageSize);

        PhotoInfoDto GetPhotoInfo(int photoId);

        void UpdatePhotoInfo(int photoId, string photoName);

        void DeletePhoto(int photoId);

        int GetPhotoCount(int? userId);
        
        void SetPhotoRating(int photoId, int userId, int rating);
        
    }
}
