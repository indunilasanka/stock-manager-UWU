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
    public class PDHeadController : ApplicationController<User>
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

        // GET: PDHead
        public ActionResult Index()
        {
            StoreService service = new StoreService();
            PDService service1 = new PDService();
            StoreDash store = new StoreDash();
            store.NoAGRN = service.SearchGRN(new GRNViewModel()).GRNList.Where(r => r.IsApproved == true).Count();
            store.NoGRN = service.SearchGRN(new GRNViewModel()).GRNList.Where(r => r.IsApproved == false).Count();
            store.NoARN = service.SearchRN(new RNViewModel()).RNList.Where(r => r.IsApproved == true).Count();
            store.NoRn = service.SearchRN(new RNViewModel()).RNList.Where(r => r.IsApproved == false).Count();
            store.NoAPO = service1.SearchPO(new POViewModel()).POList.Where(r => r.IsApproved == true).Count();
            store.NoPO = service1.SearchPO(new POViewModel()).POList.Where(r => r.IsApproved == false).Count();
            return View(store);
        }

        public ActionResult ManagePO(POViewModel Model)
        {
            PDService service = new PDService();
            ViewBag.Suppliers = new SelectList(AllSuppliers(), "Value", "Text");

            Model = service.SearchPO(Model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_POGrid", Model.POList);
            }
            return View("ManagePurchaseOrder", Model);
        }

        public ActionResult SearchPO(POViewModel Model)
        {
            PDService service = new PDService();
            Model = service.SearchPO(Model);
            return PartialView("_POGrid", Model.POList);
        }

        public ActionResult ReviewPO(int? POId)
        {
            PDService service = new PDService();
            PO user = new PO();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            user = service.GetPO(POId);
            user.POIList = service.GetPOItemList(POId);
            return View("ReviewPurchaseOrder", user);
        }

        public ActionResult GoBackPO()
        {
            return RedirectToAction("ManagePO");
        }

        public ActionResult AddEditPO(int? POId)
        {
            PDService service = new PDService();
            PO user = new PO();
            user = service.GetPO(POId);
            if (user == null)
            {
                user = new PO();
            }
            ViewBag.Suppliers = new SelectList(AllSuppliers(), "Value", "Text");
            return View("AddEditPurchaseOrder", user);
        }

        public ActionResult SavePO(PO user)
        {
            int id = 0;
            PDService service = new PDService();
            try
            {
                if (user.POId > 0)
                {
                    id = service.UpdatePO(user);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddPO(user);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeletePO(int? POId)
        {
            int id = 0;
            try
            {
                if (POId.HasValue || POId.Value > 0)
                {
                    PDService service = new PDService();
                    id = service.DeletePO(POId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult AddProduct(int? PIId)
        {
            PDService service = new PDService();
            POItemList Product = new POItemList();
            if (PIId != null)
            {
                Product = service.GetPOItem(PIId);
            }
            ViewBag.MainAssests = new SelectList(AllMainAssests(), "Value", "Text");
            ViewBag.SubAssests = new SelectList(GetSubCatogoryForMC(Product.MCId), "Value", "Text");
            ViewBag.ProductName = new SelectList(GetProductForSC(Product.SCId), "Value", "Text");
            return PartialView("_AddProduct", Product);
        }
        public ActionResult SaveProduct(POItemList product)
        {
            int id = 0;
            PDService service = new PDService();
            try
            {
                if (product.PIId > 0)
                {
                    id = service.UpdatePOItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddPOItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteProduct(int? PIId)
        {
            try
            {
                PDService service = new PDService();
                service.DeleteProduct(PIId);
                return Json(new { Status = true, Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult PrintHtmlInovice(int? POId)
        {
            PDService service = new PDService();
            PO Model = new PO();
            Model = service.GetPO(POId);
            Model.POIList = service.GetPOItemList(POId);
            return View("PO", Model);
        }

        public ActionResult PrintHtmlInovicePDF(int? POId)
        {
            PDService service = new PDService();
            PO Model = new PO();
            Model = service.GetPO(POId);
            Model.POIList = service.GetPOItemList(POId);
            return PartialView("_PO", Model);
        }

        public JsonResult AcceptPO(int? POId)
        {
            int id = 0;
            try
            {
                if (POId.HasValue || POId.Value > 0)
                {
                    PDService service = new PDService();
                    id = service.AcceptPO(POId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }



        public ActionResult ManageGRN(GRNViewModel Model)
        {
            StoreService service = new StoreService();
            ViewBag.Suppliers = new SelectList(AllSuppliers(), "Value", "Text");

            Model = service.SearchGRN(Model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_GRNGrid", Model.GRNList);
            }
            return View("ManageGRN", Model);
        }

        public ActionResult SearchGRN(GRNViewModel Model)
        {
            StoreService service = new StoreService();
            Model = service.SearchGRN(Model);
            return PartialView("_GRNGrid", Model.GRNList);
        }

        public ActionResult ReviewGRN(int? GRNId)
        {
            StoreService service = new StoreService();
            GRN user = new GRN();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            user = service.GetGRN(GRNId);
            user.GRNIList = service.GetGRNItemList(GRNId);
            return View("ReviewGRN", user);
        }

        public ActionResult GoBackGRN()
        {
            return RedirectToAction("ManageGRN");
        }

        public ActionResult AddEditGRN(int? GRNId)
        {
            StoreService service = new StoreService();
            GRN user = new GRN();
            user = service.GetGRN(GRNId);
            if (user == null)
            {
                user = new GRN();
            }
            ViewBag.Suppliers = new SelectList(AllSuppliers(), "Value", "Text");
            return View("AddEditGRN", user);
        }

        public ActionResult SaveGRN(GRN user)
        {
            int id = 0;
            StoreService service = new StoreService();
            try
            {
                if (user.GRNId > 0)
                {
                    id = service.UpdateGRN(user);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddGRN(user);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteGRN(int? GRNId)
        {
            int id = 0;
            try
            {
                if (GRNId.HasValue || GRNId.Value > 0)
                {
                    StoreService service = new StoreService();
                    id = service.DeleteGRN(GRNId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AddGRNItem(int? GIId)
        {
            StoreService service = new StoreService();
            GRNItemList Product = new GRNItemList();
            ViewBag.Purchase = new SelectList(AllPOs(), "Value", "Text");

            if (GIId != null)
            {
                Product = service.GetGRNItem(GIId);
            }
            ViewBag.PIItem = new SelectList(AllPOItem(Product.POId), "Value", "Text");
            return PartialView("_AddGRNItem", Product);
        }

        public ActionResult SaveGRNItem(GRNItemList product)
        {
            int id = 0;
            StoreService service = new StoreService();
            try
            {
                if (product.GIId > 0)
                {
                    id = service.UpdateGRNItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddGRNItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteGRNItem(int? GIId)
        {
            try
            {
                StoreService service = new StoreService();
                service.DeleteGRNItem(GIId);
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

        public ActionResult PrintHtmlGRN(int? GRNId)
        {
            StoreService service = new StoreService();
            GRN Model = new GRN();
            Model = service.GetGRN(GRNId);
            Model.GRNIList = service.GetGRNItemList(GRNId);
            return View("GRN", Model);
        }

        public ActionResult PrintHtmlGRNPDF(int? GRNId)
        {
            StoreService service = new StoreService();
            GRN Model = new GRN();
            Model = service.GetGRN(GRNId);
            Model.GRNIList = service.GetGRNItemList(GRNId);
            return PartialView("_GRN", Model);
        }

        public JsonResult AcceptGRN(int? GRNId)
        {
            int id = 0;
            try
            {
                if (GRNId.HasValue || GRNId.Value > 0)
                {
                    StoreService service = new StoreService();
                    id = service.AcceptGRN(GRNId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult AcceptRN(int? RNId)
        {
            int id = 0;
            try
            {
                if (RNId.HasValue || RNId.Value > 0)
                {
                    StoreService service = new StoreService();
                    id = service.AcceptRN(RNId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult ManageRN(RNViewModel Model)
        {
            StoreService service = new StoreService();
            ViewBag.Suppliers = new SelectList(AllSuppliers(), "Value", "Text");

            Model = service.SearchRN(Model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RNGrid", Model.RNList);
            }
            return View("ManageRN", Model);
        }

        public ActionResult SearchRN(RNViewModel Model)
        {
            StoreService service = new StoreService();
            Model = service.SearchRN(Model);
            return PartialView("_RNGrid", Model.RNList);
        }

        public ActionResult ReviewRN(int? RNId)
        {
            StoreService service = new StoreService();
            RN user = new RN();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            user = service.GetRN(RNId);
            user.RNIList = service.GetRNItemList(RNId);
            return View("ReviewRN", user);
        }

        public ActionResult GoBackRN()
        {
            return RedirectToAction("ManageRN");
        }

        public ActionResult AddEditRN(int? RNId)
        {
            StoreService service = new StoreService();
            RN user = new RN();
            user = service.GetRN(RNId);
            if (user == null)
            {
                user = new RN();
            }
            ViewBag.Suppliers = new SelectList(AllSuppliers(), "Value", "Text");
            return View("AddEditRN", user);
        }

        public ActionResult SaveRN(RN user)
        {
            int id = 0;
            StoreService service = new StoreService();
            try
            {
                if (user.RNId > 0)
                {
                    id = service.UpdateRN(user);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddRN(user);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteRN(int? RNId)
        {
            int id = 0;
            try
            {
                if (RNId.HasValue || RNId.Value > 0)
                {
                    StoreService service = new StoreService();
                    id = service.DeleteRN(RNId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AddRNItem(int? RIId)
        {
            StoreService service = new StoreService();
            RNItemList Product = new RNItemList();
            ViewBag.GRN = new SelectList(AllGRNs(), "Value", "Text");

            if (RIId != null)
            {
                Product = service.GetRNItem(RIId);
            }
            ViewBag.GRNI = new SelectList(AllGRNItem(Product.GRNId), "Value", "Text");
            return PartialView("_AddRNItem", Product);
        }

        public ActionResult SaveRNItem(RNItemList product)
        {
            int id = 0;
            StoreService service = new StoreService();
            try
            {
                if (product.RIId > 0)
                {
                    id = service.UpdateRNItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddRNItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteRNItem(int? RIId)
        {
            try
            {
                StoreService service = new StoreService();
                service.DeleteRNItem(RIId);
                return Json(new { Status = true, Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetRMItems(int? GRNId)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            Designations = AllGRNItem(GRNId);
            var results = (Designations.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { Designations = results }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRMItemsDetails(int? GIId)
        {
            StoreService service = new StoreService();
            GRNItemList user = new GRNItemList();
            user = service.GetGRNItem(GIId);
            return Json(new { up = user.GIUnitPrice, pq = user.GIPendingQuantity, qty = user.GIReceivedQuantity, }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintHtmlRN(int? RNId)
        {
            StoreService service = new StoreService();
            RN Model = new RN();
            Model = service.GetRN(RNId);
            Model.RNIList = service.GetRNItemList(RNId);
            return View("RN", Model);
        }

        public ActionResult PrintHtmlRNPDF(int? RNId)
        {
            StoreService service = new StoreService();
            RN Model = new RN();
            Model = service.GetRN(RNId);
            Model.RNIList = service.GetRNItemList(RNId);
            return PartialView("_RN", Model);
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