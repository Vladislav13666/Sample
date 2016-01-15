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
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Client.WebApp.Controllers
{
    public class UserAlbumController : Controller
    {
        private IPhotoGalleryService _service;
        const int pageSize = 2;

        public UserAlbumController(IPhotoGalleryService service)
        {
            _service = service;
        }

       [Authorize]     
       [HttpGet]  
        public ActionResult Manage(string login,int page=0)
        {
            if (login != User.Identity.Name) {
                return View("Error");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("PhotoCollection", GetItemsPage(page));
            }
            return View(GetItemsPage(page));
        }

        private IEnumerable<PhotoModel> GetItemsPage(int page)
        {
            return _service.GetUserAlbum((User as PhotoGalleryPrincipal).Id, page, pageSize).
                Select(Mapper.Map<PhotoDto, PhotoModel>);
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
            if (ModelState.IsValid)
            {
                _service.UpdatePhotoInfo(photoModel.PhotoId, photoModel.Title);
                return RedirectToAction("Manage");
            }
            return PartialView("EditPhoto",photoModel);
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
            try
            {
                _service.DeletePhoto(photo.PhotoId);
                FileInfo fileInf = new FileInfo(Server.MapPath(photo.ImagePath));
                fileInf.Delete();
                return RedirectToAction("Manage");
            }
            catch (FaultException<ServiceDataError> ex)
            {
                return Json("User attempts delete non-existent photo");
            }
        }     

    }   
}