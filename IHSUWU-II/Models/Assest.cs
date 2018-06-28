using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Assest
    {
    }

    public class MainCatogory
    {
        public int MCId { get; set; }
        public string MCName { get; set; }
        public string MCSymbol { get; set; }
    }

    public class SubCatogory
    {
        public int MCId { get; set; }
        public int SCId { get; set; }
        public string SCName { get; set; }
        public string SCSymbol { get; set; }
        public string MCName { get; set; }
        public string MCSymbol { get; set; }

    }

    public class Product
    {

        public int PROId { get; set; }
        public int MCId { get; set; }
        public int SCId { get; set; }
        public int ProNo { get; set; }
        public string SCName { get; set; }
        public string SCSymbol { get; set; }
        public string MCName { get; set; }
        public string MCSymbol { get; set; }
        public string ProName { get; set; }

    }

    public class MainCatogoryViewModel
    {
        public string MCName { get; set; }
        public string MCSymbol { get; set; }
        public List<MainCatogory> MainAssestsList { get; set; }
    }

    public class SubCatogoryViewModel
    {
        public string SCName { get; set; }
        public string SCSymbol { get; set; }
        public int MCId { get; set; }
        public List<SubCatogory> SubAssestsList { get; set; }
    }

    public class ProductViewModel
    {
        public int ProNo { get; set; }
        public int SCId { get; set; }
        public int MCId { get; set; }
        public string ProName { get; set; }
        public List<Product> ProductsList { get; set; }
    }
}