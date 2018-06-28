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
    public class PDWorkerController : ApplicationController<User>
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
        // GET: PDWorker
        public ActionResult Index()
        {
            PDService service1 = new PDService();
            StoreDash store = new StoreDash();
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

        //public ActionResult ProductGrid(int? POId)
        //{
        //    try
        //    {
        //        PDService service = new PDService();
        //        PO user = new PO();
        //        user.POIList = service.GetPOItemList(POId);
        //        return PartialView("_ProductsGrid", user.POIList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
        //    }
        //}

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