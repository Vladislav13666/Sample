using AutoMapper;
using PhotoGallery.Client.Reference.PhotoGalleryServ;
using PhotoGallery.Client.WebApp.Auth;
using PhotoGallery.Client.WebApp.Helper;
using PhotoGallery.Client.WebApp.Models.PhotoViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Client.WebApp.Controllers
{
    public class AlbumController : Controller
    {
        private IPhotoGalleryService _service;
        const int pageSize = 2;

        public AlbumController(IPhotoGalleryService service)
        {
            _service = service;
        }

               
        [HttpGet]
        [Authorize]
        public ActionResult AllPhotos(int page=0)
        {                  
            if (Request.IsAjaxRequest())
            {
                return PartialView("PhotoCollection", GetItemsPage(page));
            }
           return View(GetItemsPage(page));          
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserPhotos(int userId,int page=0)
        {           
            UserAlbum userAlbum = new UserAlbum(GetItemsPage(page, userId), userId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("PhotoCollection", GetItemsPage(page, userId));
            }
            return View(userAlbum);
        }

        private IEnumerable<PhotoModel> GetItemsPage(int page, int? userId=null)
        {
            return _service.GetPhotos((User as PhotoGalleryPrincipal).Id, userId, page, pageSize).
                Select(Mapper.Map<PhotoDto, PhotoModel>);
        }
              

        [Authorize]
        [HttpPost]
        public ActionResult SetPhotoRating(int photoId,int rating)
        {         
            _service.SetPhotoRating(photoId, (User as PhotoGalleryPrincipal).Id,rating);
            return Json("add rating");
        }       



    }
}