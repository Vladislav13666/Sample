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
        #region UserService
        void CreateUser(UserRegisterDto user);
        UserDto AuthenticateUser(string login,string password);
        UserDto FindUserByUserLogin(string login);
        UserInfoDto GetUserPublicInfo(string login);
        UserDto UpdateUserInfo(int userId, string firstName, string secondName);
        UserDto UpdateUserEmail(int userId, string newEmail);
        UserDto UpdateUserPassword(int userId, string currentPassword, string newPassword);

        #endregion

        #region PhotoService
        void AddPhoto(PhotoDto photoDto);           
        PhotoDto[] GetPhotos(int userId, int? albumOwnerId, int page, int pageSize);
        PhotoDto[] GetUserAlbum(int userId, int page, int pageSize);
        PhotoInfoDto GetPhotoInfo(int photoId);
        void UpdatePhotoInfo(int photoId, string photoName);
        void DeletePhoto(int photoId);   
        void SetPhotoRating(int photoId, int userId, int rating);

        #endregion

    }
}
