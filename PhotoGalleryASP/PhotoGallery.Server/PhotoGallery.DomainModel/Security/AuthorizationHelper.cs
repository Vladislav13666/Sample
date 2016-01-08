using PhotoGallery.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel.Security
{
   public static class AuthorizationHelper
    {
        public const int SaltSize = 20;
        public const int KeySize = 32;

        public static bool CheckPassword(string password, User user)
        {         
            using (var derivedBytes = new Rfc2898DeriveBytes(password, user.Salt))
            {
                byte[] newKey = derivedBytes.GetBytes(KeySize);
                return newKey.SequenceEqual(user.Key);
            }
        }

        public static void GenerateKey(string password, User user)
        {
            using (var derivedBytes = new Rfc2898DeriveBytes(password, SaltSize))
            {
                user.Salt = derivedBytes.Salt;
                user.Key = derivedBytes.GetBytes(KeySize);
            }
        }
    }
}
