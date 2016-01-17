using AutoMapper;
using PhotoGallery.Client.Reference.PhotoGalleryServ;
using PhotoGallery.Client.WebApp.Auth;
using PhotoGallery.Client.WebApp.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PhotoGallery.Client.WebApp.Controllers
{
    public class UserController : Controller
    {
        private IPhotoGalleryService _service;

        public UserController(IPhotoGalleryService service)
        {
            _service = service;
        }
       
        
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

       [HttpPost]
        public ActionResult Register(RegisterUserModel registerUserModel)
        {
            if (ModelState.IsValid)
            {
                var userRegister = Mapper.Map<RegisterUserModel, UserRegisterDto>(registerUserModel);
                try
                {
                    _service.CreateUser(userRegister);
                     Authorize(userRegister.Login, userRegister.Password);
                    return RedirectToAction("AllPhotos", "Album");
                }
                catch (FaultException<ServiceValidationError> ex)
                {
                    ModelState.AddModelError("Login", "Login or Email exists");
                    ModelState.AddModelError("Email", "Login or Email exists");
                }
            }

            return View(registerUserModel);
        }

        [HttpPost]
        public ActionResult LogIn(LoginUserModel loginUserModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!Authorize(loginUserModel.Login, loginUserModel.Password))
                    {
                        ModelState.AddModelError("", "Unable to log in. Please check that you have entered your login and password correctly");
                        return View(loginUserModel);
                    }
                    return RedirectToAction("AllPhotos", "Album");
                }
                catch (FaultException<ServiceValidationError> ex)
                {
                    ModelState.AddModelError("Password", "Wrong password");                 
                }
                catch (FaultException<ServiceDataError> ex)
                {
                    ModelState.AddModelError("Login", "Unexisting login");
                }
            }
            return View(loginUserModel);
        }
    

    private bool Authorize(string userName, string password)
        {
            var authUser = _service.AuthenticateUser(userName, password);
            if (authUser == null)
                return false;
            var serializePrincipal = Mapper.Map<UserDto, PhotoGalleryPrincipalSerializeModel>(authUser);
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(serializePrincipal);

            var authTicket = new FormsAuthenticationTicket(
                1,
                authUser.Login,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(authCookie);
            return true;
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "User");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile(string login)
        {
            if (login != User.Identity.Name)
            {
               return RedirectToAction("AllPhotos", "Album") ;
            }
            var userInfo = _service.FindUserByUserLogin(login);
            ViewData["Info"] = Mapper.Map<UserDto, EditUserInfo>(userInfo);
            ViewData["Password"] = Mapper.Map<UserDto, EditUserPassword>(userInfo);
            ViewData["Email"] = Mapper.Map<UserDto, EditUserEmail>(userInfo);       
            return View();
        }

        [HttpPost]
        [Authorize]       
        public ActionResult ChangeUserEmail(EditUserEmail editUserMail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userEmailDto=_service.UpdateUserEmail(editUserMail.Id, editUserMail.Email);
                    var userEmail=Mapper.Map<UserDto, EditUserEmail>(userEmailDto);
                    return PartialView("ChangeUserEmail", userEmail);
                }
                catch (FaultException<ServiceValidationError> ex) {
                    ModelState.AddModelError("Email", "user with such email already exists");
                }              
            }
                      
            return PartialView("ChangeUserEmail", editUserMail);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeUserPassword(EditUserPassword editUserPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDto=_service.UpdateUserPassword(editUserPassword.Id, editUserPassword.CurrentPassword, editUserPassword.NewPassword);
                    var userPasswordModel = Mapper.Map<UserDto, EditUserPassword>(userDto);
                    return PartialView("ChangeUserPassword", userPasswordModel);
                }
                catch (FaultException<ServiceValidationError>)
                {
                    ModelState.AddModelError("CurrentPassword", "wrong password");
                }            
            }
            return PartialView("ChangeUserPassword", editUserPassword);


        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeUserInfo(EditUserInfo editUserInfo)
        {
            if (ModelState.IsValid)
            {
                var editInfoDto = _service.UpdateUserInfo(editUserInfo.Id, editUserInfo.FirstName, editUserInfo.LastName);
                var edit = Mapper.Map<UserDto, EditUserInfo>(editInfoDto);
                return PartialView("ChangeUserInfo", edit);
            }            
            return Json("invalid model");

        }


    }
}