using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class LoginReviewDetailSet
    {
        public AuthContext authContext { get; set; }

        public List<int> functionIds { get; set; }

    }
}
