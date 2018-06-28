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
    public class GAdminHeadController : Controller
    {
        // GET: GAdminHead
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageSerial(SerialViewModel Model)
        {
            PDService service = new PDService();
            Model = service.SearchSerial(Model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SerialGrid", Model.SerialList);
            }
            return View("ManageSerial", Model);
        }

        public ActionResult ManageComplain(SerialViewModel Model)
        {
            PDService service = new PDService();
            //Model = service.SearchSerial(Model);


            return View("ManageComplain", Model);
        }

    }
}