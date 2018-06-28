using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Models.Constants;
using Login.Service;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Login.Controllers
{
    public class AdminController : ApplicationController<User>
    {

        public ActionResult AddComplain()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string image, string cs)
        {
            image = image.Substring("data:image/png;base64,".Length);
            var buffer = Convert.FromBase64String(image);
            // TODO: I am saving the image on the hard disk but
            // you could do whatever processing you want with it
            Random rnd = new Random();
            int no = rnd.Next(1000);
            System.IO.File.WriteAllBytes(Server.MapPath("~/App_Data/" + no + ".jpg"), buffer);
            getDecode(no);
            return Json(new { success = true });
        }


        public ActionResult getDecode(int no)
        {
            QRCodeEncoder enc = new QRCodeEncoder();
            QRCodeDecoder dec = new QRCodeDecoder();
            //string aaa = (dec.decode(new QRCodeBitmapImage(qrcode)));

            Bitmap b = new Bitmap(Path.Combine(Server.MapPath("/App_Data/" + no + ".jpg")));
            string aaaaa = (dec.decode(new QRCodeBitmapImage(b)));

            return Json(new { success = true });
        }
        // GET: Admin
        public ActionResult Index()
        {
            UserService service = new UserService();
            AdminDash admin = new AdminDash();
            admin.NoofLocations = service.SearchLocations(new LocationViewModel()).LocationList.Count;
            admin.NoofUsers = service.SearchUsers(new UserViewModel()).UserList.Count;
            admin.NoofSuppliers = service.SearchSuppliers(new SupplierViewModel()).SupplierList.Count;
            admin.NoofProducts = service.SearchProducts(new ProductViewModel()).ProductsList.Count;
            admin.NoofMainCat = service.SearchMainAssests(new MainCatogoryViewModel()).MainAssestsList.Count;
            admin.NoOfSubCat = service.SearchSubAssests(new SubCatogoryViewModel()).SubAssestsList.Count;
            return View(admin);
        }

        public ActionResult ManageUser(UserViewModel userModel)
        {
            UserService service = new UserService();
            ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            userModel = service.SearchUsers(userModel);
            User sessionModel = GetLogOnSessionModel();
            var itemToRemove = userModel.UserList.Single(r => r.UserId == sessionModel.UserId);
            userModel.UserList.Remove(itemToRemove);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserGrid", userModel.UserList);
            }
            return View("ManageUser", userModel);
        }

        [HttpPost]
        public ActionResult SearchUsers(UserViewModel userModel)
        {
            UserService service = new UserService();
            userModel = service.SearchUsers(userModel);
            User sessionModel = GetLogOnSessionModel();
            var itemToRemove = userModel.UserList.Single(r => r.UserId == sessionModel.UserId);
            userModel.UserList.Remove(itemToRemove);
            return PartialView("_UserGrid", userModel.UserList);
        }

        public ActionResult ReviewUser(int? UserId)
        {
            UserService service = new UserService();
            User user = new User();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            user = service.GetUser(UserId);

            return View("ReviewUser", user);
        }

        public ActionResult GoBackUser()
        {
            return RedirectToAction("ManageUser");
        }

        public ActionResult AddEditUser(int? UserId)
        {
            UserService service = new UserService();
            User user = new User();
            user = service.GetUser(UserId);
            if (user == null)
            {
                user = new User();
            }
            ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");
            ViewBag.Designations = new SelectList(AllDesignation(user.Division), "Value", "Text");
            return View("AddEditUser", user);
        }

        public JsonResult GetDesignations(int? Division)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            Designations = AllDesignation(Division);
            var results = (Designations.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { Designations = results }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUser(User user)
        {
            int id = 0;
            UserService service = new UserService();
            try
            {
                if (user.UserId > 0)
                {
                    id = service.UpdateUser(user);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddUser(user);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteUser(int? UserId)
        {
            int id = 0;
            try
            {
                if (UserId.HasValue || UserId.Value > 0)
                {
                    UserService service = new UserService();
                    id = service.DeleteUser(UserId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ManageLocation(LocationViewModel locationModel)
        {
            UserService service = new UserService();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            locationModel = service.SearchLocations(locationModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_LocationGrid", locationModel.LocationList);
            }
            return View("ManageLocation", locationModel);
        }

        [HttpPost]
        public ActionResult SearchLocation(LocationViewModel locationModel)
        {
            UserService service = new UserService();
            locationModel = service.SearchLocations(locationModel);
            return PartialView("_LocationGrid", locationModel.LocationList);
        }

        public ActionResult ReviewLocation(int? LocationId)
        {
            UserService service = new UserService();
            Location location = new Location();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            location = service.GetLocation(LocationId);

            return View("ReviewLocation", location);
        }

        public ActionResult GoBackLocation()
        {
            return RedirectToAction("ManageLocation");
        }

        public ActionResult AddEditLocation(int? LocationId)
        {
            UserService service = new UserService();
            Location location = new Location();
            location = service.GetLocation(LocationId);
            if (location == null)
            {
                location = new Location();
            }
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");
            //ViewBag.Designations = new SelectList(AllDesignation(user.Division), "Value", "Text");
            return View("AddEditLocation", location);
        }

        public ActionResult SaveLocation(Location location)
        {
            int id = 0;
            UserService service = new UserService();
            try
            {
                if (location.LocationId > 0)
                {
                    id = service.UpdateLocation(location);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddLocation(location);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteLocation(int? LocationId)
        {
            int id = 0;
            try
            {
                if (LocationId.HasValue || LocationId.Value > 0)
                {
                    UserService service = new UserService();
                    id = service.DeleteLocation(LocationId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ManageSupplier(SupplierViewModel supplierModel)
        {
            UserService service = new UserService();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            supplierModel = service.SearchSuppliers(supplierModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SupplierGrid", supplierModel.SupplierList);
            }
            return View("ManageSupplier", supplierModel);
        }

        [HttpPost]
        public ActionResult SearchSupplier(SupplierViewModel supplierModel)
        {
            UserService service = new UserService();
            supplierModel = service.SearchSuppliers(supplierModel);
            return PartialView("_SupplierGrid", supplierModel.SupplierList);
        }

        public ActionResult ReviewSupplier(int? SId)
        {
            UserService service = new UserService();
            Supplier supplier = new Supplier();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            supplier = service.GetSupplier(SId);

            return View("ReviewSupplier", supplier);
        }

        public ActionResult GoBackSupplier()
        {
            return RedirectToAction("ManageSupplier");
        }

        public ActionResult AddEditSupplier(int? SId)
        {
            UserService service = new UserService();
            Supplier supplier = new Supplier();
            supplier = service.GetSupplier(SId);
            if (supplier == null)
            {
                supplier = new Supplier();
            }
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");
            //ViewBag.Designations = new SelectList(AllDesignation(user.Division), "Value", "Text");
            return View("AddEditSupplier", supplier);
        }

        public ActionResult SaveSupplier(Supplier supplier)
        {
            int id = 0;
            UserService service = new UserService();
            try
            {
                if (supplier.SId > 0)
                {
                    id = service.UpdateSupplier(supplier);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddSupplier(supplier);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteSupplier(int? SId)
        {
            int id = 0;
            try
            {
                if (SId.HasValue || SId.Value > 0)
                {
                    UserService service = new UserService();
                    id = service.DeleteSupplier(SId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ManageMainAssests(MainCatogoryViewModel mainAssestsModel)
        {
            UserService service = new UserService();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            mainAssestsModel = service.SearchMainAssests(mainAssestsModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MainCategoryGrid", mainAssestsModel.MainAssestsList);
            }
            return View("ManageMainAssests", mainAssestsModel);
        }
        [HttpPost]
        public ActionResult SearchMainAssests(MainCatogoryViewModel mainAssestsModel)
        {
            UserService service = new UserService();
            mainAssestsModel = service.SearchMainAssests(mainAssestsModel);
            return PartialView("_MainCategoryGrid", mainAssestsModel.MainAssestsList);
        }

        public ActionResult ReviewMainCatogory(int? MCId)
        {
            UserService service = new UserService();
            MainCatogory assest = new MainCatogory();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            assest = service.GetMainAssest(MCId);

            return View("ReviewMainAssest", assest);
        }

        public ActionResult GoBackMainAssest()
        {
            return RedirectToAction("ManageMainAssests");
        }

        public ActionResult AddEditMainAssest(int? MCId)
        {
            UserService service = new UserService();
            MainCatogory assest = new MainCatogory();
            assest = service.GetMainAssest(MCId);
            if (assest == null)
            {
                assest = new MainCatogory();
            }
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");
            //ViewBag.Designations = new SelectList(AllDesignation(user.Division), "Value", "Text");
            return View("AddEditMainAssest", assest);
        }

        public ActionResult SaveMainAssest(MainCatogory assest)
        {
            int id = 0;
            UserService service = new UserService();
            try
            {
                if (assest.MCId > 0)
                {
                    id = service.UpdateMainAssest(assest);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddMainAssest(assest);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteMainAssest(int? MCId)
        {
            int id = 0;
            try
            {
                if (MCId.HasValue || MCId.Value > 0)
                {
                    UserService service = new UserService();
                    id = service.DeleteMainAssest(MCId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ManageSubAssests(SubCatogoryViewModel AssestsModel)
        {
            UserService service = new UserService();
            ViewBag.MainAssests = new SelectList(AllMainAssests(), "Value", "Text");

            AssestsModel = service.SearchSubAssests(AssestsModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubCategoryGrid", AssestsModel.SubAssestsList);
            }
            return View("ManageSubAssests", AssestsModel);
        }

        [HttpPost]
        public ActionResult SearchSubAssests(SubCatogoryViewModel AssestsModel)
        {
            UserService service = new UserService();
            AssestsModel = service.SearchSubAssests(AssestsModel);
            return PartialView("_SubCategoryGrid", AssestsModel.SubAssestsList);
        }

        public ActionResult ReviewSubCatogory(int? SCId)
        {
            UserService service = new UserService();
            SubCatogory assest = new SubCatogory();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            assest = service.GetSubAssest(SCId);

            return View("ReviewSubAssest", assest);
        }

        public ActionResult GoBackSubAssest()
        {
            return RedirectToAction("ManageSubAssests");
        }

        public ActionResult AddEditSubAssest(int? SCId)
        {
            UserService service = new UserService();
            SubCatogory assest = new SubCatogory();
            assest = service.GetSubAssest(SCId);
            if (assest == null)
            {
                assest = new SubCatogory();
            }
            ViewBag.MainAssests = new SelectList(AllMainAssests(), "Value", "Text");
            //ViewBag.Designations = new SelectList(AllDesignation(user.Division), "Value", "Text");
            return View("AddEditSubAssest", assest);
        }

        public ActionResult SaveSubAssest(SubCatogory assest)
        {
            int id = 0;
            UserService service = new UserService();
            try
            {
                if (assest.SCId > 0)
                {
                    id = service.UpdateSubAssest(assest);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddSubAssest(assest);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteSubAssest(int? SCId)
        {
            int id = 0;
            try
            {
                if (SCId.HasValue || SCId.Value > 0)
                {
                    UserService service = new UserService();
                    id = service.DeleteSubAssest(SCId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ManageProducts(ProductViewModel AssestsModel)
        {
            UserService service = new UserService();
            ViewBag.MainAssests = new SelectList(AllMainAssests(), "Value", "Text");

            AssestsModel = service.SearchProducts(AssestsModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductGrid", AssestsModel.ProductsList);
            }
            return View("ManageProducts", AssestsModel);
        }

        [HttpPost]
        public ActionResult SearchProducts(ProductViewModel AssestsModel)
        {
            UserService service = new UserService();
            AssestsModel = service.SearchProducts(AssestsModel);
            return PartialView("_ProductGrid", AssestsModel.ProductsList);
        }

        public ActionResult ReviewProduct(int? PROId)
        {
            UserService service = new UserService();
            Product assest = new Product();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            assest = service.GetProduct(PROId);

            return View("ReviewProduct", assest);
        }

        public ActionResult GoBackProduct()
        {
            return RedirectToAction("ManageProducts");
        }

        public ActionResult AddEditProduct(int? PROId)
        {
            UserService service = new UserService();
            Product assest = new Product();
            assest = service.GetProduct(PROId);
            if (assest == null)
            {
                assest = new Product();
            }
            ViewBag.MainAssests = new SelectList(AllMainAssests(), "Value", "Text");
            ViewBag.SubAssests = new SelectList(GetSubCatogoryForMC(assest.MCId), "Value", "Text");
            return View("AddEditProduct", assest);
        }

        public ActionResult SaveProduct(Product assest)
        {
            int id = 0;
            UserService service = new UserService();
            try
            {
                if (assest.PROId > 0)
                {
                    id = service.UpdateProduct(assest);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddProduct(assest);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProduct(int? PROId)
        {
            int id = 0;
            try
            {
                if (PROId.HasValue || PROId.Value > 0)
                {
                    UserService service = new UserService();
                    id = service.DeleteProduct(PROId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetSubCatogory(int? MC)
        {
            List<SelectListItem> SubCatogory = new List<SelectListItem>();
            SubCatogory = GetSubCatogoryForMC(MC);
            var results = (SubCatogory.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { SubCatogory = results }, JsonRequestBehavior.AllowGet);
        }
    }
}