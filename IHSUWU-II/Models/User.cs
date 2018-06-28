using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class User
    {
        public string UserName { set; get; }
        public string LogErrorMsg { set; get; }
        public string Password { set; get; }
        public int Division { set; get; }
        public string DivisionName { set; get; }
        public int Designation { set; get; }
        public string DesignationName { set; get; }
        public string Email { set; get; }
        public string FullName { set; get; }
        public int UserId { set; get; }
        public bool IsPasswordChanged { set; get; }
    }

    public class AdminDash
    {
        public int NoofUsers { set; get; }
        public int NoofLocations { set; get; }
        public int NoofSuppliers { set; get; }
        public int NoofProducts { set; get; }
        public int NoofMainCat { set; get; }
        public int NoOfSubCat { set; get; }

    }


    public class UserViewModel
    {
        public int Division { set; get; }
        public string Email { set; get; }
        public string FullName { set; get; }
        public List<User> UserList { get; set; }

    }
}