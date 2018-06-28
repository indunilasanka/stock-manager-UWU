using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    using System.Security.Principal;
    using System.Web;
    /// <summary>
    /// This represents Context information that will be passed across different layers. 
    /// Currently Context object includes authenticated user ID.
    /// </summary>
    public class Context
    {
        #region Public Constructors

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Context()
        {
            ClientID = 0;
            UserID = 0;
        }

        /// <summary>
        /// Overloaded Constructor.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="userName"></param>
        public Context(int userId, string userName)
        {
            ClientID = 0;
            this.UserID = userId;
            this.UserName = userName;
        }

        #endregion 

        #region Public Properties

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>The client ID.</value>
        public int ClientID { get; set; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        /// <value>The user name.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the type of the user authentication.
        /// </summary>
        /// <value>The type of the user authentication.</value>
        public string UserAuthenticationType { get; set; }

        #endregion
    }
}
