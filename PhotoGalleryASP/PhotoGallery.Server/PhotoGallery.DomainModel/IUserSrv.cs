using PhotoGallery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel
{
  public interface IUserSrv
    {       
        void CreateUser(UserRegisterDto user);
        UserDto AuthenticateUser(string login,string password);
        UserDto FindUserByUserLogin(string login);
        UserInfoDto GetUserPublicInfo(string login);
        UserDto UpdateUserInfo(int userId, string firstName, string secondName);
        UserDto UpdateUserEmail(int userId, string newEmail);
        UserDto UpdateUserPassword(int userId, string currentPassword, string newPassword);

    }
}
