using Login.Models;
using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Service
{
    public class AuthenticationService
    {
        private RepositoryBase<NavotarDbContext> _repository;
        private SecurityRepository __repository2;
        public AuthenticationService()
        {
            _repository = new UserRepository();
            __repository2 = new SecurityRepository();
        }

        public List<Menu> GetMenus()
        {
            try
            {
                return _repository.DataContext.tblMenus.ToList();
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }

        }

        public Model.UserContext AuthenticateUser(string userName, string password)
        {

            Model.UserContext newUserContext = null;

            User users = _repository.GetList<User>(u => u.UserName == userName && u.Password == password).FirstOrDefault();
            if (users != null && users.UserID > 0)
            {
                AuthContext context = new AuthContext();
                Client client = new Client();
                client = _repository.GetList<Client>(u => u.ClientId == users.ClientId).FirstOrDefault();
                if (client != null)
                {
                    context.Address = client.Address1;
                    context.ClientID = client.ClientId;
                    context.ClientName = client.ClientName;
                    context.Country = client.CountryId.HasValue ? client.CountryId.Value : 0;
                    context.CustomizationFolder = client.CustomizationFolder;
                    context.Email = client.Email;
                    context.FirstName = users.FirstName;
                    context.LastName = users.LastName;
                    context.MasterPage = client.MasterPage;
                    context.Phone = client.Phone;
                    context.UserRoleID = users.UserRoleID;
                    context.StateId = client.StateId;
                    context.Theme = client.Theme;
                    context.UserID = users.UserID;
                    context.UserName = users.UserName;
                    context.Culture = client.Culture;
                    context.TimeZone = client.TimeZone;
                }
                context.FunctionList = _repository.DataContext.tblRoleFunctionMapping.Where(u => u.UserRoleID == context.UserRoleID).Select(m => m.FunctionID).ToList();

                newUserContext = new Model.UserContext(context);
            }
            return newUserContext;
        }

        public LoginReviewDetailSet GetApiConsumerUser(Guid clientId)
        {
            try
            {
                LoginReviewDetailSet detail_list = new LoginReviewDetailSet();


                detail_list = __repository2.GetApiConsumerUser(clientId);

                return detail_list;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool CheckFeatureForUser(FeatureType featureType, UserProfile context)
        {
            bool flag = context.FeatureList.Contains((int)featureType);
            return flag;
        }

        public static string GetFeatureAttributeForUser(FeatureType featureType, UserProfile context)
        {
            FeatureServices com = new FeatureServices(context.UserAuthContext);
            string value = com.GetFeaturesValue(context.UserAuthContext.ClientID, (int)featureType);
            return value;
        }

        //login user

        public LoginReviewDetailSet GetLoginReviewSet(Login login)
        {
            try
            {
                LoginReviewDetailSet detail_list = new LoginReviewDetailSet();


                detail_list = __repository2.GetLoginReviewSet(login);

                return detail_list;
            }

            catch (Exception ex)
            {
                throw;
            }
        }


        public bool IsValidApiConsumer(Guid clientId, string clientSecret)
        {
            try
            {
                return __repository2.IsValidApiConsumer(clientId, clientSecret);
            }
            catch
            {
                throw;
            }
        }

        //public bool validUuser(Login lg)
        //{
        //   // User user = null;
        //    bool IsuserExist = false;
        //    var username = lg.UserName;
        //    var password = lg.Password;

        //    //IsuserExist = _repository.DataContext.tblUser.Where(m => m.UserName == username && m.Password == password).ToList().Count > 0;

        //    int count = (from h in _repository.DataContext.tblUser
        //                     where h.UserName.Equals(lg.UserName)
        //                     where h.Password.Equals(lg.Password)
        //                     select h.UserID).Count();
        //    if(count>0)
        //    {
        //        IsuserExist = true;
        //    }

        //    if (IsuserExist)
        //    {
        //    //   int clientID = Convert.ToInt32( _repository.DataContext.tblUser.Where(m => m.UserName == username && m.Password == password).Select(m => m.ClientId));
        //        var clientID = (from h in _repository.DataContext.tblUser
        //                     where h.UserName.Equals(lg.UserName)
        //                     where h.Password.Equals(lg.Password)
        //                     select h.ClientId).FirstOrDefault();
        //       lg.clientId = clientID;
        //    }
        //    return IsuserExist;

        //}

        public List<int> GetAllClientId(int typeId) //Return list of clientId according to the given 'AutoGenerationSetUp' type
        {
            try
            {
                return __repository2.GetAllClientId(typeId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public LoginReviewDetailSet GetUserAuthInfo(int clientId) //Return authentication information according to the given clientId
        {
            try
            {
                LoginReviewDetailSet detail_list = new LoginReviewDetailSet();


                detail_list = __repository2.GetUserAuthInfo(clientId);

                return detail_list;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
