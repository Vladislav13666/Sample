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
        int pageSize = 2;

        public AlbumController(IPhotoGalleryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AllPhotos(int? userId = null,int page = 1)
        {           
            var photos = _service.GetPhotos((User as PhotoGalleryPrincipal).Id,userId,page, pageSize).
                Select(Mapper.Map<PhotoDto, PhotoModel>);
            var count=_service.GetPhotoCount(userId);
            var data = new PageableData<PhotoModel>(photos, page, count, pageSize,userId);
            return View(data);

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