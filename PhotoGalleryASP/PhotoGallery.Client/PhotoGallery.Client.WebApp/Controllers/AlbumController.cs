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

        public AlbumController(IPhotoGalleryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult AllPhotos(int page = 1)
        {
            int pageSize = 1;
            var photos = _service.GetPhotos((User as PhotoGalleryPrincipal).Id,page, pageSize).
                Select(Mapper.Map<PhotoDto, PhotoModel>);
            var count=_service.GetPhotoCount(null);
            var data = new PageableData<PhotoModel>(photos, page, count, pageSize);
            return View(data);

        }

        [HttpGet]
        public ActionResult UserPhotos(int userId,int page=1)
        {
            int pageSize = 1;
            var photos=_service.GetPhotosByUserId(userId, (User as PhotoGalleryPrincipal).Id,page, pageSize).Select(Mapper.Map<PhotoDto, PhotoModel>);
            var count = _service.GetPhotoCount(userId);
            var data = new PageableData<PhotoModel>(photos, page, count, pageSize,userId);
            return View(data);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult SetPhotoRating(int photoId,int rating)
        {         
            _service.SetPhotoRating(photoId, (User as PhotoGalleryPrincipal).Id,rating);
            return RedirectToAction("AllPhotos");
        }

        



    }
}