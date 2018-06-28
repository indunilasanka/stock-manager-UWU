using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Login.Repository
{
    public class DepartmentRepository : RepositoryBase<UserContext>
    {
        public DepartmentRepository()
        {
        }

        public RequestViewModel SearchRequest(RequestViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            RequestViewModel Details = new RequestViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    Details.RequestList = db.Query<Requests>("exec usp_SearchRequest " + "@LocationId",
                            new { LocationId = model.LocationId }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return Details;
        }

        public Requests GetRequest(int? RequestId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            Requests user = new Requests();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<Requests>("exec usp_GetRequest " + "@RequestId",
                            new { RequestId = RequestId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return user;
        }

        public RequestItem GetRequestItem(int? RequestItemId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            RequestItem user = new RequestItem();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<RequestItem>("exec usp_GetRequestItem " + "@RequestItemId",
                            new { RequestItemId = RequestItemId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return user;
        }

        public int UpdateRequest(Requests user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditRequest " +
                             " @RequestId,@RequestDate,@LocationId,@RequestStatus; select @@ret; ",
                      new
                      {
                          @RequestId = user.RequestId,
                          @RequestDate = user.RequestDate,
                          @LocationId = user.LocationId,
                          @RequestStatus = user.RequestStatus

                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }

        public int UpdateRequestItem(RequestItem user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditRequestItem " +
                             " @RequestItemId,@RequestId,@PROId,@RequestQuantity,@RequestDescription; select @@ret; ",
                      new
                      {
                          @RequestItemId = user.RequestItemId,
                          @RequestId = user.RequestId,
                          @PROId = user.PROId,
                          @RequestQuantity = user.RequestQuantity,
                          @RequestDescription = user.RequestDescription

                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }

        public int AddRequest(Requests user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddRequest " + " @RequestDate,@LocationId,@RequestStatus; select @@ret;",
                        new
                        {
                            @RequestDate = user.RequestDate,
                            @LocationId = user.LocationId,
                            @RequestStatus = user.RequestStatus

                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }
        public int AddRequestItem(RequestItem user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddRequestItem " + " @RequestId,@PROId,@RequestQuantity,@RequestDescription; select @@ret;",
                        new
                        {
                            @RequestId = user.RequestId,
                            @PROId = user.PROId,
                            @RequestQuantity = user.RequestQuantity,
                            @RequestDescription = user.RequestDescription
                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }

        public int DeleteRequest(int? RequestId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteRequest" + " @RequestId", new { RequestId = RequestId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int DeleteRequestItem(int? RequestItemId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteRequestItem" + " @RequestItemId", new { RequestItemId = RequestItemId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public List<RequestItem> GetRequestItemList(int? RequestId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<RequestItem> user = new List<RequestItem>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<RequestItem>("exec usp_GetRequestItemList " + "@RequestId",
                            new { RequestId = RequestId }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return user;
        }

    }
}