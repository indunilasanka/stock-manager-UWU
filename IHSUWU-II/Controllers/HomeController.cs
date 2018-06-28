using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Models.Constants;
using Login.Service;

namespace Login.Controllers
{
    public class HomeController : ApplicationController<User>
    {

        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        public ActionResult Login()
        {
            Session[SessionConstants.SESSION_CONTEXT_INSTANCE] = null;
            User lg = new User();
            return View(lg);
        }

        [HttpPost]
        public JsonResult Login(User model)
        {

            UserService service = new UserService();
            var UserDetails = service.CheckUserExits(model);
            //Create Log on session
            SetLogOnSessionModel(UserDetails);
            // User sessionModel = GetLogOnSessionModel();
            if ((UserDetails == null))
            {
                //No User
                return Json(new { Status = "error", Message = "Incorrect Username or Password" }, JsonRequestBehavior.AllowGet);
            }
            else if (UserDetails.UserId == -1)
            {
                UserDetails.LogErrorMsg = "Check Your Connection";
                return Json(new { Status = "error", Message = "Check Your Connection" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                Session[SessionConstants.SESSION_CONTEXT_INSTANCE] = UserDetails;

                if (UserDetails.Designation == 1)
                {
                    return Json(new { Status = "url", Message = "Admin/Index" }, JsonRequestBehavior.AllowGet);
                }
                else if (UserDetails.Division == 2)
                {

                    return Json(new { Status = "url", Message = "StoreKeeper/Index" }, JsonRequestBehavior.AllowGet);

                    //else
                    //{
                    //    return Json(new { Status = "url", Message = "StoreWorker/Index" }, JsonRequestBehavior.AllowGet);
                    //}
                }
                else if (UserDetails.Division == 3)
                {
                    if (UserDetails.Designation == 4)
                    {
                        return Json(new { Status = "url", Message = "GAdminBoS/Index" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (UserDetails.Designation == 5)
                    {
                        return Json(new { Status = "url", Message = "GAdminHead/Index" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Status = "url", Message = "GAdminWorker/Index" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (UserDetails.Division == 4)
                {
                    if (UserDetails.Designation == 7)
                    {
                        return Json(new { Status = "url", Message = "PDHead/Index" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Status = "url", Message = "PDWorker/Index" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (UserDetails.Division == 5)
                {
                    return Json(new { Status = "url", Message = "Department/Index" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.LogErrorMsg = "We are not Support this type of User";
                    return Json(new { Status = "error", Message = "We are not Support this type of User" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult LogOut()
        {
            //destroy session
            AbandonSession();
            return RedirectToAction("Login", "Home");
        }

    }
}