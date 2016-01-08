using AutoMapper;
using PhotoGallery.Client.Reference.PhotoGalleryServ;
using PhotoGallery.Client.WebApp.Auth;
using PhotoGallery.Client.WebApp.Helper;
using PhotoGallery.Client.WebApp.Models.PhotoViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Client.WebApp.Controllers
{
    public class UserAlbumController : Controller
    {
        private IPhotoGalleryService _service;
        
       
        public UserAlbumController(IPhotoGalleryService service)
        {
            _service = service;
        }

       [Authorize]     
       [HttpGet]  
        public ActionResult Manage(int page=1)
        {
            int pageSize = 1;           
            var photos = _service.GetUserAlbum((User as PhotoGalleryPrincipal).Id, page, pageSize).
                Select(Mapper.Map<PhotoDto, PhotoModel>);
            var count = _service.GetPhotoCount((User as PhotoGalleryPrincipal).Id);
            var data = new PageableData<PhotoModel>(photos, page, count, pageSize);
            return View(data);
        }
              


        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return PartialView("CreatePhoto");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadPhoto(UploadPhotoModel pm)
        {        
            if (ModelState.IsValid)
            {
                var currentTime = DateTime.Now;            
                string filePath = "/Files/"+ currentTime.ToString("dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".")+Path.GetFileName(pm.File.FileName);
                pm.File.SaveAs(Server.MapPath(filePath));
                var photoDto = new PhotoDto
                {
                    UserId = (User as PhotoGalleryPrincipal).Id,
                    CreateTime = currentTime,
                    PhotoOwner = User.Identity.Name,
                    Title = pm.Title,
                    AverageRating = 0,
                    CurrentUserRating = 0,
                    ImagePath = filePath
                };

                _service.AddPhoto(photoDto);
                return RedirectToAction("Manage");
            }
            return PartialView("CreatePhoto",pm);             
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditPhoto(int photoId)
        {
            var photoInfoDto=_service.GetPhotoInfo(photoId);
            var photoEdit=Mapper.Map<PhotoInfoDto, PhotoEditModel>(photoInfoDto);
            return PartialView("EditPhoto", photoEdit);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditPhoto(PhotoEditModel photoModel)
        {
             _service.UpdatePhotoInfo(photoModel.PhotoId, photoModel.Title);
             return RedirectToAction("Manage");
        }
              
        [HttpGet]
        [Authorize]
        public ActionResult DeletePhoto(int photoId)
        {
            var photoInfo=_service.GetPhotoInfo(photoId);                    
            return PartialView("DeletePhoto", Mapper.Map<PhotoInfoDto, PhotoDeleteModel>(photoInfo));
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeletePhoto(PhotoDeleteModel photo)
        {
            _service.DeletePhoto(photo.PhotoId);
            FileInfo fileInf = new FileInfo(Server.MapPath(photo.ImagePath));
            fileInf.Delete();
            return RedirectToAction("Manage");
        }

       


    }


   
}