using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class StoreDash
    {
        public int NoAGRN { set; get; }
        public int NoGRN { set; get; }
        public int NoRn { set; get; }
        public int NoARN { set; get; }
        public int NoAI { set; get; }
        public int NoI { set; get; }
        public int NoAPO { set; get; }
        public int NoPO { set; get; }
    }

    public class GRN
    {
        public int GRNId { get; set; }
        public string GRNNo { get; set; }
        public DateTime? GRNCreatedDate { get; set; }
        public int SId { get; set; }
        public decimal? GRNTotal { get; set; }
        public string SName { get; set; }
        public string SAddress { get; set; }
        public string SEmail { get; set; }
        public string SMobile { get; set; }
        public bool IsApproved { get; set; } = false;
        public List<GRNItemList> GRNIList { get; set; }
    }

    public class GRNViewModel
    {
        public int SId { get; set; }
        public List<GRN> GRNList { get; set; }

    }

    public class GRNItemList
    {
        public int GRNId { get; set; }
        public int GIId { get; set; }
        public int POId { get; set; }
        public int PIId { get; set; }
        public string GIDescription { get; set; }
        public string PONo { get; set; }
        public int GIReceivedQuantity { get; set; }
        public int GIPendingQuantity { get; set; }
        public decimal? GIUnitPrice { get; set; }
        public decimal? GITotal { get; set; }
        public string ProName { get; set; }
        public int RQ { get; set; }
        public int ProNo { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsGenerated { get; set; } = false;
    }

    public class Issue
    {
        public int IId { get; set; }
        public string IStatus { get; set; }
        public DateTime? ICreatedDate { get; set; }
        public DateTime? IApprovedDate { get; set; }
        public int LocationId { get; set; }
        public decimal? Total { get; set; }
        public string LocationName { get; set; }
        public string LocationSymbol { get; set; }
        public bool IsApproved { get; set; } = false;
        public List<IssueItem> IssueItemList { get; set; }
    }

    public class IssueViewModel
    {
        public int LocationId { get; set; }
        public List<Issue> IssueList { get; set; }

    }

    public class IssueItem
    {
        public int IItemId { get; set; }
        public int IId { get; set; }
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
        public int RequestItemId { get; set; }
        public int PROId { get; set; }
        public int IssuedQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal? ItemUnitPrice { get; set; }
        public decimal? ItemTotal { get; set; }
        public string ProName { get; set; }
        public bool IsApproved { get; set; } = false;
        public int ProNo { get; set; }
    }


    public class Serial
    {
        public int ii_no { get; set; }
        public int I_no { get; set; }
        public int Serial_ID { get; set; }
        public string Serial_no { get; set; }
        public string asset_Status { get; set; }
        public int PROId { get; set; }
        public string ProName { get; set; }
        public int ProNo { get; set; }
    }

    public class SerialViewModel
    {
        public List<Serial> SerialList { get; set; }
    }

    public class RN
    {
        public int RNId { get; set; }
        public string RNNo { get; set; }
        public DateTime? RNCreatedDate { get; set; }
        public DateTime? RNApprovedDate { get; set; }
        public int SId { get; set; }
        public decimal? RNTotal { get; set; }
        public string SName { get; set; }
        public string RNStatus { get; set; }
        public string SAddress { get; set; }
        public string SEmail { get; set; }
        public string SMobile { get; set; }
        public bool IsApproved { get; set; } = false;
        public List<RNItemList> RNIList { get; set; }
    }

    public class RNViewModel
    {
        public int SId { get; set; }
        public List<RN> RNList { get; set; }

    }

    public class RNItemList
    {
        public int RNId { get; set; }
        public int RIId { get; set; }
        public int GRNId { get; set; }
        public int GIId { get; set; }
        public int RIQuantity { get; set; }
        public string RIDescription { get; set; }
        public decimal? RIUnitPrice { get; set; }
        public decimal? RITotal { get; set; }
        public string GIDescription { get; set; }
        public int ProNo { get; set; }
        public int GIReceivedQuantity { get; set; }
        public int GIPendingQuantity { get; set; }
        public decimal? GIUnitPrice { get; set; }
        public decimal? GITotal { get; set; }
        public bool IsApproved { get; set; } = false;
    }

    public class QR
    {
        public int? QRId { set; get; }
        public int? GRNId { set; get; }
        public int? GIId { set; get; }
        public int GIReceivedQuantity { get; set; }
        public int GIPendingQuantity { get; set; }
        public string QRCode { set; get; }
        public string PONo { get; set; }

    }
}