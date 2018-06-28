using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Requests
    {
        public int RequestId { get; set; }
        public DateTime? RequestDate { get; set; }
        public int LocationId { get; set; }
        public string RequestStatus { get; set; }
        public string RequestNo { get; set; }
        public string LocationName { set; get; }
        public string LocationSymbol { set; get; }
        public List<RequestItem> RequestItemList { get; set; }
    }
    public class RequestViewModel
    {
        public int LocationId { get; set; }
        public List<Requests> RequestList { get; set; }

    }
    public class RequestItem
    {
        public int RequestId { get; set; }
        public int RequestItemId { get; set; }
        public int MCId { get; set; }
        public int SCId { get; set; }
        public int PROId { get; set; }
        public int RequestQuantity { get; set; }
        public string RequestDescription { get; set; }
        public string ProName { get; set; }
        public int ProNo { get; set; }
    }
}