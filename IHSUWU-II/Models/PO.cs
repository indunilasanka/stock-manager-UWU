using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class PO
    {
        public int POId { get; set; }
        public string PONo { get; set; }
        public DateTime? POCreatedDate { get; set; }
        public int SId { get; set; }
        public decimal? POTotalWithoutVat { get; set; }
        public decimal? POVat { get; set; }
        public decimal? POTotal { get; set; }
        public DateTime? PODueDate { get; set; }
        public string POTenderNo { get; set; }
        public string POFileNo { get; set; }
        public string POStatus { get; set; }
        public string SName { get; set; }
        public string SAddress { get; set; }
        public string SEmail { get; set; }
        public string SMobile { get; set; }
        public List<POItemList> POIList { get; set; }
        public DateTime? POQuatationDate { get; set; }
        public bool IsApproved { get; set; } = false;
    }

    public class POViewModel
    {
        public int SId { get; set; }
        public string POTenderNo { get; set; }
        public string POFileNo { get; set; }
        public List<PO> POList { get; set; }

    }

    public class POItemList
    {
        public int POId { get; set; }
        public int PIId { get; set; }
        public int PROId { get; set; }
        public int MCId { get; set; }
        public int SCId { get; set; }
        public string PIDescription { get; set; }
        public int PIQuantity { get; set; }
        public decimal? PIUnitPrice { get; set; }
        public decimal? PITotal { get; set; }
        public string ProName { get; set; }
        public int ProNo { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}