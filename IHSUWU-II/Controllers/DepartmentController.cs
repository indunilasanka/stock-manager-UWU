using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Models.Constants;
using Login.Service;
using System.Configuration;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Login.Controllers
{
    public class DepartmentController : ApplicationController<User>
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

        // GET: StoreHead
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageRequest(RequestViewModel Model)
        {
            DepartmentService service = new DepartmentService();
            ViewBag.Locations = new SelectList(AllLocations(), "Value", "Text");

            Model = service.SearchRequest(Model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RequestGrid", Model.RequestList);
            }
            return View("ManageRequest", Model);
        }

        public ActionResult SearchRequest(RequestViewModel Model)
        {
            DepartmentService service = new DepartmentService();
            Model = service.SearchRequest(Model);
            return PartialView("_RequestGrid", Model.RequestList);
        }

        public ActionResult ReviewRequest(int? RequestId)
        {
            DepartmentService service = new DepartmentService();
            Requests user = new Requests();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            user = service.GetRequest(RequestId);
            user.RequestItemList = service.GetRequestItemList(RequestId);
            return View("ReviewRequest", user);
        }

        public ActionResult GoBackRequest()
        {
            return RedirectToAction("ManageRequest");
        }

        public ActionResult AddEditRequest(int? RequestId)
        {
            DepartmentService service = new DepartmentService();
            Requests user = new Requests();
            user = service.GetRequest(RequestId);
            if (user == null)
            {
                user = new Requests();
            }
            ViewBag.Locations = new SelectList(AllLocations(), "Value", "Text");
            return View("AddEditRequest", user);
        }

        public ActionResult SaveRequest(Requests user)
        {
            int id = 0;
            DepartmentService service = new DepartmentService();
            try
            {
                if (user.RequestId > 0)
                {
                    id = service.UpdateRequest(user);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddRequest(user);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteRequest(int? RequestId)
        {
            int id = 0;
            try
            {
                if (RequestId.HasValue || RequestId.Value > 0)
                {
                    DepartmentService service = new DepartmentService();
                    id = service.DeleteRequest(RequestId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AddRequestItem(int? RequestItemId)
        {
            DepartmentService service = new DepartmentService();
            RequestItem Product = new RequestItem();
            if (RequestItemId != null)
            {
                Product = service.GetRequestItem(RequestItemId);
            }
            ViewBag.MainAssests = new SelectList(AllMainAssests(), "Value", "Text");
            ViewBag.SubAssests = new SelectList(GetSubCatogoryForMC(Product.MCId), "Value", "Text");
            ViewBag.Products = new SelectList(GetProductForSC(Product.SCId), "Value", "Text");
            return PartialView("_AddRequestItem", Product);
        }

        public ActionResult SaveRequestItem(RequestItem product)
        {
            int id = 0;
            DepartmentService service = new DepartmentService();
            try
            {
                if (product.RequestItemId > 0)
                {
                    id = service.UpdateRequestItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddRequestItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteRequestItem(int? RequestItemId)
        {
            try
            {
                DepartmentService service = new DepartmentService();
                service.DeleteRequestItem(RequestItemId);
                return Json(new { Status = true, Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetGRMItems(int? POId)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            Designations = AllPOItem(POId);
            var results = (Designations.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { Designations = results }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGRMItemsDetails(int? PIId)
        {
            PDService service = new PDService();
            POItemList user = new POItemList();
            user = service.GetPOItem(PIId);
            return Json(new { up = user.PIUnitPrice, des = user.PIDescription, qty = user.PIQuantity }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintHtmlRequest(int? RequestId)
        {
            DepartmentService service = new DepartmentService();
            Requests Model = new Requests();
            Model = service.GetRequest(RequestId);
            Model.RequestItemList = service.GetRequestItemList(RequestId);
            return View("REQUEST", Model);
        }

        public ActionResult PrintHtmlRequestPDF(int? RequestId)
        {
            DepartmentService service = new DepartmentService();
            Requests Model = new Requests();
            Model = service.GetRequest(RequestId);
            Model.RequestItemList = service.GetRequestItemList(RequestId);
            return PartialView("_REQUEST", Model);
        }

        public JsonResult GetSubCatogory(int? MC)
        {
            List<SelectListItem> SubCatogory = new List<SelectListItem>();
            SubCatogory = GetSubCatogoryForMC(MC);
            var results = (SubCatogory.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { SubCatogory = results }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetProductSC(int? MC)
        {
            List<SelectListItem> SubCatogory = new List<SelectListItem>();
            SubCatogory = GetProductForSC(MC);
            var results = (SubCatogory.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { SubCatogory = results }, JsonRequestBehavior.AllowGet);
        }

    }
}