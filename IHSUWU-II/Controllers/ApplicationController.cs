using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Service;
using System.Web.Routing;

namespace Login.Controllers
{
    public class ApplicationController<TSource> : Controller
    {
        private const string LogOnSession = "LogOnSession";  //session index name
        private const string ErrorController = "Error";      //session independent controller
        private const string LogOnController = "Home";      //session independent and LogOn controller    
        private const string LogOnAction = "Login";          //action to rederect

        protected ApplicationController()
        {
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            /*important to check both, because logOn and error Controller should be access able with out any session*/
            if (!IsNonSessionController(requestContext) && !HasSession())
            {
                //Rederect to logon action
                Rederect(requestContext, Url.Action(LogOnAction, LogOnController));
            }
        }

        private bool IsNonSessionController(RequestContext requestContext)
        {
            var currentController = requestContext.RouteData.Values["controller"].ToString().ToLower();
            var nonSessionedController = new List<string>() { ErrorController.ToLower(), LogOnController.ToLower() };
            return nonSessionedController.Contains(currentController);
        }

        private void Rederect(RequestContext requestContext, string action)
        {
            requestContext.HttpContext.Response.Clear();
            requestContext.HttpContext.Response.Redirect(action);
            requestContext.HttpContext.Response.End();
        }

        protected bool HasSession()
        {
            return Session[LogOnSession] != null;
        }

        protected TSource GetLogOnSessionModel()
        {
            return (TSource)this.Session[LogOnSession];
        }

        protected void SetLogOnSessionModel(TSource model)
        {
            Session[LogOnSession] = model;
        }

        protected void AbandonSession()
        {
            if (HasSession())
            {
                Session.Abandon();
            }
        }

        public List<SelectListItem> AllDivisions()
        {
            List<SelectListItem> Divisions = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Divisions> DivisionList = service.GetAllDivisions();
            Divisions.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in DivisionList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.DivisionName,
                    Value = item.Division.ToString()
                };

                Divisions.Add(newItem);
            }

            return Divisions;
        }
        public List<SelectListItem> AllLocations()
        {
            List<SelectListItem> Divisions = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Location> DivisionList = service.AllLocations();
            Divisions.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in DivisionList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.LocationName + " - " + item.LocationSymbol,
                    Value = item.LocationId.ToString()
                };

                Divisions.Add(newItem);
            }

            return Divisions;
        }
        public List<SelectListItem> AllSuppliers()
        {
            List<SelectListItem> Suppliers = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Supplier> SupplierList = service.GetAllSuppliers();
            Suppliers.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in SupplierList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.SName,
                    Value = item.SId.ToString()
                };

                Suppliers.Add(newItem);
            }

            return Suppliers;
        }

        public List<SelectListItem> AllProducts()
        {
            List<SelectListItem> Suppliers = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Product> SupplierList = service.AllProducts();
            Suppliers.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in SupplierList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.ProName,
                    Value = item.PROId.ToString()
                };

                Suppliers.Add(newItem);
            }

            return Suppliers;
        }

        public List<SelectListItem> AllPOs()
        {
            List<SelectListItem> Suppliers = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<PO> SupplierList = service.AllPOs();
            Suppliers.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in SupplierList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.PONo,
                    Value = item.POId.ToString()
                };

                Suppliers.Add(newItem);
            }

            return Suppliers;
        }

        public List<SelectListItem> AllGRNs()
        {
            List<SelectListItem> Suppliers = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<GRN> SupplierList = service.AllGRNs();
            Suppliers.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in SupplierList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.GRNNo,
                    Value = item.GRNId.ToString()
                };

                Suppliers.Add(newItem);
            }

            return Suppliers;
        }


        public List<SelectListItem> AllMainAssests()
        {
            List<SelectListItem> MainAssests = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<MainCatogory> assestList = service.AllMainAssests();
            MainAssests.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in assestList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.MCName + " ( " + item.MCSymbol + " )",
                    Value = item.MCId.ToString()
                };

                MainAssests.Add(newItem);
            }

            return MainAssests;
        }

        public List<SelectListItem> AllRequests()
        {
            List<SelectListItem> MainAssests = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Requests> assestList = service.AllRequests();
            MainAssests.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });
            foreach (var item in assestList)
            {
                var newItem = new SelectListItem
                {
                    Text = item.RequestNo,
                    Value = item.RequestId.ToString()
                };

                MainAssests.Add(newItem);
            }

            return MainAssests;
        }

        public List<SelectListItem> AllDesignation(int? Division)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Designations> DesignationsList = new List<Designations>();

            Designations.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            if ((Division != null) & (Division != 0))
            {
                DesignationsList = service.GetAllDesignations(Division);

                foreach (var item in DesignationsList)
                {
                    var newItem = new SelectListItem
                    {
                        Text = item.DesignationName,
                        Value = item.Designation.ToString()
                    };

                    Designations.Add(newItem);
                }
            }

            return Designations;
        }

        public List<SelectListItem> AllRequestItems(int? ID)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<RequestItem> DesignationsList = new List<RequestItem>();

            Designations.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            if ((ID != null) & (ID != 0))
            {
                DesignationsList = service.AllRequestItems(ID);

                foreach (var item in DesignationsList)
                {
                    var newItem = new SelectListItem
                    {
                        Text = item.RequestDescription,
                        Value = item.RequestItemId.ToString()
                    };

                    Designations.Add(newItem);
                }
            }

            return Designations;
        }


        public List<SelectListItem> AllPOItem(int? POId)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<POItemList> DesignationsList = new List<POItemList>();

            Designations.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            if ((POId != null) & (POId != 0))
            {
                DesignationsList = service.AllPOItem(POId);

                foreach (var item in DesignationsList)
                {
                    var newItem = new SelectListItem
                    {
                        Text = item.PIDescription,
                        Value = item.PIId.ToString()
                    };

                    Designations.Add(newItem);
                }
            }

            return Designations;
        }

        public List<SelectListItem> AllGRNItem(int? GRNId)
        {
            List<SelectListItem> Designations = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<GRNItemList> DesignationsList = new List<GRNItemList>();

            Designations.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            if ((GRNId != null) & (GRNId != 0))
            {
                DesignationsList = service.AllGRNItem(GRNId);

                foreach (var item in DesignationsList)
                {
                    var newItem = new SelectListItem
                    {
                        Text = item.GIDescription,
                        Value = item.GIId.ToString()
                    };

                    Designations.Add(newItem);
                }
            }

            return Designations;
        }

        public List<SelectListItem> GetSubCatogoryForMC(int? MC)
        {
            List<SelectListItem> SubCatogory = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<SubCatogory> SubCatogoryList = new List<SubCatogory>();

            SubCatogory.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            if ((MC != null) & (MC != 0))
            {
                SubCatogoryList = service.GetSubCatogory(MC);

                foreach (var item in SubCatogoryList)
                {
                    var newItem = new SelectListItem
                    {
                        Text = item.SCName + " (" + item.SCSymbol + ") ",
                        Value = item.SCId.ToString()
                    };

                    SubCatogory.Add(newItem);
                }
            }

            return SubCatogory;
        }

        public List<SelectListItem> GetProductForSC(int? SC)
        {
            List<SelectListItem> SubCatogory = new List<SelectListItem>();
            CommonService service = new CommonService();
            List<Product> SubCatogoryList = new List<Product>();

            SubCatogory.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });

            if ((SC != null) & (SC != 0))
            {
                List<Product> SupplierList = service.AllProducts();
                for (int i = 0; i < SupplierList.Count; i++)
                {
                    if (SupplierList[i].SCId == SC)
                    {
                        SubCatogoryList.Add(SupplierList[i]);
                    }
                }

                foreach (var item in SubCatogoryList)
                {
                    var newItem = new SelectListItem
                    {
                        Text = item.ProName,
                        Value = item.PROId.ToString()
                    };

                    SubCatogory.Add(newItem);
                }
            }

            return SubCatogory;
        }
    }
}