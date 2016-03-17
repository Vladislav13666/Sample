using PhotoGallery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DomainModel
{
    public interface IPhotoSrv
    {       
        void AddPhoto(PhotoDto photoDto);
        PhotoDto[] GetPhotos(int userId, int? albumOwnerId, int page, int pageSize);
        PhotoDto[] GetUserAlbum(int userId, int page, int pageSize);
        PhotoInfoDto GetPhotoInfo(int photoId);
        void UpdatePhotoInfo(int photoId, string photoName);
        void DeletePhoto(int photoId);
        void SetPhotoRating(int photoId, int userId, int rating);
       
    }
}
