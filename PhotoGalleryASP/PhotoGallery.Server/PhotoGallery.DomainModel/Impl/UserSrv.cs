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
    public class UserSrv : IUserSrv
    {
        private PhotoGalleryDbContext db;
        public UserSrv(PhotoGalleryDbContext context)
        {
            db = context;
        }
                  
        public void CreateUser(UserRegisterDto user)
        {
            if(!db.Users.Any(u=>u.Login==user.Login) && !db.Users.Any(u => u.Email == user.Email))
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
                throw new MissingDataException(login);
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
                throw new MissingDataException(login);
            }               
            return Mapper.Map<User, UserDto>(user);
        }

        public UserInfoDto GetUserPublicInfo(string login)
        {
            var user = db.GetUserByUserLogin(login);
            if (user == null)
            {
                throw new MissingDataException(login);
            }
            return Mapper.Map<User, UserInfoDto>(user);
        }


        public UserDto UpdateUserEmail(int userId, string newEmail)
        {
            var oldUser = db.GetUserByUserId(userId);
            if (oldUser == null)
            {
                throw new MissingDataException("user not found");
            }
            if (db.Users.Any(u => u.Email== newEmail))
            {
                throw new UserDataException(newEmail);
            }
            oldUser.Email = newEmail;
            db.Entry(oldUser).State = EntityState.Modified;
            db.SaveChanges();
            return Mapper.Map<User, UserDto>(oldUser);         
        }

        public UserDto UpdateUserPassword(int userId,string currentPassword,string newPassword)
        {
               var oldUser = db.GetUserByUserId(userId);
                if (oldUser == null)
                {
                    throw new MissingDataException("user not found");
                }
            if (AuthorizationHelper.CheckPassword(currentPassword, oldUser))
            {
                AuthorizationHelper.GenerateKey(newPassword,oldUser);
                db.Entry(oldUser).State = EntityState.Modified;
                db.SaveChanges();
                return Mapper.Map<User, UserDto>(oldUser);
            }
            else
            {
                throw new UserDataException(currentPassword);
            }         
                      
        }

        public UserDto UpdateUserInfo(int userId,string firstName,string secondName)
        {             
           var oldUser = db.GetUserByUserId(userId);
            if (oldUser == null)
            {
              throw new MissingDataException("user not found");
            }
            oldUser.FirstName = firstName;
            oldUser.LastName = secondName;
            db.Entry(oldUser).State = EntityState.Modified;
            db.SaveChanges();
            return Mapper.Map<User,UserDto>(oldUser);
        }    
               
    }
}
       


