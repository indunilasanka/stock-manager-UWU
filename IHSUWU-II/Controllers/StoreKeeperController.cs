using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Models.Constants;
using Login.Service;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace Login.Controllers
{
    public class StoreKeeperController : ApplicationController<User>
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
            Bitmap c = ResizeBitmap(b);
            string aaaaa = (dec.decode(new QRCodeBitmapImage(c)));

            return Json(new { success = true });
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP)
        {
            Bitmap result = new Bitmap(116, 116);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, 116, 116);
            return result;
        }

        // GET: StoreHead
        public ActionResult Index()
        {
            StoreService service = new StoreService();
            StoreDash store = new StoreDash();
            store.NoAGRN = service.SearchGRN(new GRNViewModel()).GRNList.Where(r => r.IsApproved == true).Count();
            store.NoGRN = service.SearchGRN(new GRNViewModel()).GRNList.Where(r => r.IsApproved == false).Count();
            store.NoARN = service.SearchRN(new RNViewModel()).RNList.Where(r => r.IsApproved == true).Count();
            store.NoRn = service.SearchRN(new RNViewModel()).RNList.Where(r => r.IsApproved == false).Count();
            store.NoAI = service.SearchIssue(new IssueViewModel()).IssueList.Where(r => r.IsApproved == true).Count();
            store.NoI = service.SearchIssue(new IssueViewModel()).IssueList.Where(r => r.IsApproved == false).Count();
            return View(store);
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
            if (user == null) { user = new GRN(); }
            user.GRNIList = service.GetGRNItemList(GRNId);
            if (user.GRNIList == null)
            {
                user.GRNIList = new List<GRNItemList>();
            }
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


        public ActionResult ManageIssue(IssueViewModel Model)
        {
            StoreService service = new StoreService();
            ViewBag.Locations = new SelectList(AllLocations(), "Value", "Text");

            Model = service.SearchIssue(Model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_IssueGrid", Model.IssueList);
            }
            return View("ManageIssue", Model);
        }

        public ActionResult SearchIssue(IssueViewModel Model)
        {
            StoreService service = new StoreService();
            Model = service.SearchIssue(Model);
            return PartialView("_IssueGrid", Model.IssueList);
        }

        public ActionResult ReviewIssue(int? IId)
        {
            StoreService service = new StoreService();
            Issue user = new Issue();
            //ViewBag.Divisions = new SelectList(AllDivisions(), "Value", "Text");

            user = service.GetIssue(IId);
            user.IssueItemList = service.GetIssueItemList(IId);
            return View("ReviewIssue", user);
        }

        public ActionResult GoBackIssue()
        {
            return RedirectToAction("ManageIssue");
        }

        public ActionResult AddEditIssue(int? IId)
        {
            StoreService service = new StoreService();
            Issue user = new Issue();
            user = service.GetIssue(IId);
            if (user == null)
            {
                user = new Issue();
            }
            ViewBag.Locations = new SelectList(AllLocations(), "Value", "Text");
            return View("AddEditIssue", user);
        }

        public ActionResult SaveIssue(Issue user)
        {
            int id = 0;
            StoreService service = new StoreService();
            try
            {
                if (user.IId > 0)
                {
                    id = service.UpdateIssue(user);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddIssue(user);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteIssue(int? IId)
        {
            int id = 0;
            try
            {
                if (IId.HasValue || IId.Value > 0)
                {
                    StoreService service = new StoreService();
                    id = service.DeleteIssue(IId);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AddIssueItem(int? IItemId)
        {
            StoreService service = new StoreService();
            IssueItem Product = new IssueItem();

            if (IItemId != null)
            {
                Product = service.GetIssueItem(IItemId);
            }
            ViewBag.Request = new SelectList(AllRequests(), "Value", "Text");
            ViewBag.RItem = new SelectList(AllRequestItems(Product.RequestId), "Value", "Text");
            return PartialView("_AddIssueItem", Product);
        }

        public ActionResult SaveIssueItem(IssueItem product)
        {
            int id = 0;
            StoreService service = new StoreService();
            try
            {
                if (product.IItemId > 0)
                {
                    id = service.UpdateIssueItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    id = service.AddIssueItem(product);
                    return Json(new { Status = true, Id = id, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteIssueItem(int? IItemId)
        {
            try
            {
                StoreService service = new StoreService();
                service.DeleteIssueItem(IItemId);
                return Json(new { Status = true, Message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetIssueItems(int? RequestId)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            Designations = AllRequestItems(RequestId);
            var results = (Designations.Select(m => new { id = m.Value, name = m.Text })).ToList();
            return Json(new { Designations = results }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIssueItemsDetails(int? RequestItemId)
        {
            DepartmentService service = new DepartmentService();
            RequestItem user = new RequestItem();
            user = service.GetRequestItem(RequestItemId);
            return Json(new { up = user.ProName, des = user.PROId, re = user.RequestQuantity }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintHtmlIssueNote(int? IId)
        {
            StoreService service = new StoreService();
            Issue Model = new Issue();
            Model = service.GetIssue(IId);
            Model.IssueItemList = service.GetIssueItemList(IId);
            return View("IN", Model);
        }

        public ActionResult PrintHtmlIssueNotePDF(int? IId)
        {
            StoreService service = new StoreService();
            Issue Model = new Issue();
            Model = service.GetIssue(IId);
            Model.IssueItemList = service.GetIssueItemList(IId);
            return PartialView("_IN", Model);
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

        public ActionResult QRGenaration(int? GIId)
        {
            StoreService service = new StoreService();
            QR qr = new QR();
            GRNItemList gi = new GRNItemList();
            gi = service.GetGRNItem(GIId);
            qr.GIId = GIId;
            qr.GRNId = gi.GRNId;
            qr.PONo = gi.PONo;
            qr.GIReceivedQuantity = gi.GIReceivedQuantity;
            return View("QRGenaration", qr);
        }

        public ActionResult GetQR(int? GRNId, int? GIId, int? i)
        {
            string qr = GIId.ToString() + "-" + GRNId.ToString() + "-" + i.ToString();
            StoreService service = new StoreService();
            service.UpdateQR(GRNId, GIId, qr);
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(qr, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;

            var resultStream = new FileStreamResult(memoryStream, "image/png");
            resultStream.FileDownloadName = String.Format("{0}.png", "qr");

            return resultStream;
            return Json(new { Status = true, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BarcodeImage(String barcodeText)
        {
            // generating a barcode here. Code is taken from QrCode.Net library
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(barcodeText, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;

            var resultStream = new FileStreamResult(memoryStream, "image/png");
            resultStream.FileDownloadName = String.Format("{0}.png", "qr");

            return resultStream;
        }

        public ActionResult getEncode(string msg)
        {
            string yourcode = msg;
            QRCodeEncoder enc = new QRCodeEncoder();
            Bitmap qrcode = enc.Encode(yourcode);
            Image tt = qrcode as Image;

            QRCodeDecoder dec = new QRCodeDecoder();
            //string aaa = (dec.decode(new QRCodeBitmapImage(qrcode)));
            Bitmap b = new Bitmap(Path.Combine(Server.MapPath("/App_Data/HelloWorlddgfhjkljdfsgdgkjgdhfgjkgdhfjhjklhgdfhgkljfhj.jpg")));
            string aaaaa = (dec.decode(new QRCodeBitmapImage(b)));

            return View();
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
    }
}